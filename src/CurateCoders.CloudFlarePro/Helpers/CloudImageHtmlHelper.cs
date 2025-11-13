using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Umbraco.Extensions;
using CurateCoders.CloudFlarePro.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace CurateCoders.CloudFlarePro.Helpers
{
    public static class CloudImageHtmlHelper
    {
        /// <summary>
        /// Get the CloudFlare Image Tag in HTML
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="options"></param>
        /// <returns>Html Image Tag</returns>
        public static IHtmlContent GetCloudflareImageTag(this IHtmlHelper htmlHelper, CloudflareImageOptions options)
        {
            var optionsAsString = GetOptionsAsString(options.Options);

            var img = new TagBuilder("img")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };

            
            //create the mediaQueriesFromString
            var cloudImageMediaQueryList = new List<CloudImageMediaQuery>();
            var cloudImageMediaQueriesList = options.SrcSetSpec.Split(',');

            foreach (var query in cloudImageMediaQueriesList)
            {
                var parts = query.Split(':');
                cloudImageMediaQueryList.Add(new CloudImageMediaQuery() { Key = parts[0].ToString()+"w", Width = parts[1].ToString(), Height = parts[2].ToString() });
            }

            var cloudImage = new CloudImage(options.Src, options.Alt, options.Width, options.Height, options.Quality, options.LazyLoad, optionsAsString, options.Sizes, cloudImageMediaQueryList, options.CssClasses);

            img.MergeAttribute("srcset", GetMediaQueriesAsString(options.CdnZoneUrl, cloudImage));

            if (options.LazyLoad)
            {
                img.MergeAttribute("loading", "lazy");
            }

            if (!string.IsNullOrEmpty(options.Sizes))
            {
                img.MergeAttribute("sizes", options.Sizes);
            }

            if (!string.IsNullOrEmpty(options.CssClasses))
            {
                img.MergeAttribute("class", options.CssClasses);
            }

            img.MergeAttribute("src", GetImageCdnUrl(options.CdnZoneUrl, cloudImage, options.Width, options.Height));
            img.MergeAttribute("width", cloudImage.Width.ToString());
            img.MergeAttribute("height", cloudImage.Height.ToString());
            img.MergeAttribute("alt", cloudImage.Alt);
            if (!cloudImage.LazyLoad) img.MergeAttribute("fetchpriority", "high");

            string result;
            using (var writer = new StringWriter())
            {
                img.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();

                return new HtmlString(result);
            }
        }

        /// <summary>
        /// Get the CloudFlare Image Tag in HTML
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudImage"></param>
        /// <returns>Html Image Tag</returns>
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

            if (!string.IsNullOrEmpty(cloudImage.CssClasses))
            {
                img.MergeAttribute("class", cloudImage.CssClasses);
            }

            img.MergeAttribute("src", GetImageCdnUrl(cdnZoneUrl, cloudImage, cloudImage.Width, cloudImage.Height));
            img.MergeAttribute("width", cloudImage.Width.ToString());
            img.MergeAttribute("height", cloudImage.Height.ToString());
            img.MergeAttribute("alt", cloudImage.Alt);
            if (!cloudImage.LazyLoad) img.MergeAttribute("fetchpriority", "high");

            string result;
            using (var writer = new StringWriter())
            {
                img.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();

                return new HtmlString(result);
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
            return $"{cdnZoneUrl}cdn-cgi/image/{GetOptionsAsString(cloudImage.Options)},width={width},height={height},quality={cloudImage.Quality}/{cloudImage.ImageUrl.Replace(cdnZoneUrl, string.Empty)}";
        }

        /// <summary>
        /// GetOptionsAsString
        /// </summary>
        /// <param name="cloudImageOptionsDictionary"></param>
        /// <returns>ImageOptions as a String</returns>
        private static string GetOptionsAsString(object cloudImageOptionsDictionary)
        {
            string options = string.Empty;
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
            var srcset = new StringBuilder();
            foreach (var m in cloudImage.MediaQueries)
            {
                var imageCdnUrl = GetImageCdnUrl(cdnZoneUrl, cloudImage, Int32.Parse(m.Width), Int32.Parse(m.Height));
                srcset.AppendLine(imageCdnUrl + "  " + m.Key + ",");
            }
            return srcset.ToString();
        }
    }
}
