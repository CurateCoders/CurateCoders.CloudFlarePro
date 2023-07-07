using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Umbraco.Extensions;
using CurateCoders.CloudFlarePro.Models;

namespace CurateCoders.CloudFlarePro.Helpers
{
    public static class CloudImageHtmlHelper
    {
        /// <summary>
        /// Get the CloudFlare Image Tag in HTML
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudImage"></param>
        /// <returns></returns>
        public static IHtmlContent GetCloudImageTag(this IHtmlHelper htmlHelper, string cdnZoneUrl, CloudImage cloudImage)
        {
            var options = GetOptionsAsString(cloudImage.Options);

            var img = new TagBuilder("img")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };

            if (cloudImage.MediaQueries != null)
            {
                img.MergeAttribute("srcset", GetMediaQueriesAsString(cdnZoneUrl, cloudImage));
            }
            
            if (cloudImage.LazyLoad)
            {
                img.MergeAttribute("loading", "lazy");
            }

            if (!string.IsNullOrEmpty(cloudImage.Sizes))
            {
                img.MergeAttribute("sizes", cloudImage.Sizes);
            }

            img.MergeAttribute("src", GetImageCdnUrl(cdnZoneUrl, cloudImage, cloudImage.Width, cloudImage.Height));
            img.MergeAttribute("width", cloudImage.Width.ToString());
            img.MergeAttribute("height", cloudImage.Height.ToString());
            img.MergeAttribute("alt", cloudImage.Alt);

            string result;
            using (var writer = new StringWriter())
            {
                img.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();

                return new HtmlString(result.ToString());
            }
        }

        /// <summary>
        /// GetImageCdnUrl
        /// </summary>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Cloudflare Image Resizer Service optimised imageUrl</returns>
        public static string GetImageCdnUrl(string cdnZoneUrl, CloudImage cloudImage, int width, int height)
        {
            return cdnZoneUrl + "cdn-cgi/image/" + GetOptionsAsString(cloudImage.Options) + ",width=" + width.ToString() + ",height=" + height.ToString() + ",quality=" + cloudImage.Quality + "/" + cloudImage.ImageUrl.Replace(cdnZoneUrl, String.Empty);
        }

        /// <summary>
        /// GetOptionsAsString
        /// </summary>
        /// <param name="cloudImageOptionsDictionary"></param>
        /// <returns>ImageOptions as a String</returns>
        private static string GetOptionsAsString(object cloudImageOptionsDictionary)
        {
            string options = String.Empty;
            if (cloudImageOptionsDictionary != null)
            {
                options = new RouteValueDictionary(cloudImageOptionsDictionary).ToQueryString().Replace("&", ",");
            }
            return options;
        }

        /// <summary>
        /// GetMediaQueriesAsString
        /// </summary>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudImage"></param>
        /// <returns></returns>
        private static string GetMediaQueriesAsString(string cdnZoneUrl, CloudImage cloudImage)
        {
            StringBuilder srcset = new StringBuilder();
            foreach (var m in cloudImage.MediaQueries)
            {
                var imageCdnUrl = GetImageCdnUrl(cdnZoneUrl, cloudImage, Int32.Parse(m.Width), Int32.Parse(m.Height));
                srcset.AppendLine(imageCdnUrl + "  " + m.Key + ",");
            }
            return srcset.ToString();
        }
    }
}
