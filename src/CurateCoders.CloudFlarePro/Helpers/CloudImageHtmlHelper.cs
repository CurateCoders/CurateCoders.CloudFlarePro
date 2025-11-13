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
            var img = new TagBuilder("img")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };

            var cloudflareImage = new CloudflareImage(options.Src, options.Alt, options.Width, options.Height, options.Quality, options.LazyLoad, options.Options, options.Sizes, options.SrcSetSpec, options.CssClasses);

            img.MergeAttribute("srcset", GetMediaQueriesAsString(options.CdnZoneUrl, cloudflareImage));

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

            img.MergeAttribute("src", GetCloudflareImageCdnUrl(options.CdnZoneUrl, cloudflareImage, options.Width, options.Height));
            img.MergeAttribute("width", cloudflareImage.Width);
            img.MergeAttribute("height", cloudflareImage.Height);
            img.MergeAttribute("alt", cloudflareImage.Alt);

            if (!cloudflareImage.LazyLoad) img.MergeAttribute("fetchpriority", "high");

            string result;
            using (var writer = new StringWriter())
            {
                img.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();

                return new HtmlString(result);
            }
        }

        /// <summary>
        /// GetMediaQueriesAsString
        /// </summary>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudImage"></param>
        /// <returns></returns>
        private static string GetMediaQueriesAsString(string? cdnZoneUrl, CloudflareImage cloudflareImage)
        {
            var srcset = new StringBuilder();
            if (cloudflareImage.SrcSetSpec != null)
            {
                foreach (var query in cloudflareImage.SrcSetSpec.Split(','))
                {
                    var parts = query.Split(':');
                    //Key = parts[0].ToString()+"w", Width = parts[1].ToString(), Height = parts[2].ToString()
                    var imageCdnUrl = GetCloudflareImageCdnUrl(cdnZoneUrl, cloudflareImage, parts[1], parts[2]);
                    srcset.AppendLine(imageCdnUrl + "  " + parts[0] + ",");
                }
            }
            return srcset.ToString();
        }

        /// <summary>
        /// GetImageCdnUrl
        /// </summary>
        /// <param name="cdnZoneUrl"></param>
        /// <param name="cloudflareImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Cloudflare Image Resizer Service optimised imageUrl</returns>
        public static string GetCloudflareImageCdnUrl(string? cdnZoneUrl, CloudflareImage cloudflareImage, string? width, string? height)
        {
            if(!string.IsNullOrEmpty(cloudflareImage.Options))
                return $"{cdnZoneUrl}cdn-cgi/image/{cloudflareImage.Options},width={width},height={height},quality={cloudflareImage.Quality}/{cloudflareImage.Src.Replace(cdnZoneUrl, string.Empty)}";
            else
                return $"{cdnZoneUrl}cdn-cgi/image/width={width},height={height},quality={cloudflareImage.Quality}/{cloudflareImage.Src.Replace(cdnZoneUrl, string.Empty)}";
        }


        #region Original Object Based - To be Depreceated

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

        #endregion
    }
}
