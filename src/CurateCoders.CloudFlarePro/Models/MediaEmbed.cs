using Umbraco.Cms.Core.Models.PublishedContent;

namespace CurateCoders.CloudFlarePro.Models
{
	public class MediaEmbed
	{
		/// <summary>
		/// UmbracoImage reference as IPublishedContent
		/// </summary>
		public IPublishedContent UmbracoImage { get; set; }

		/// <summary>
		/// Mobile / default image width
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Mobile / default image height
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Image compression quality
		/// </summary>
		public int Quality { get; set; }

		/// <summary>
		/// LazyLoad image
		/// </summary>
		public bool LazyLoad { get; set; }

		/// <summary>
		/// List of Options: https://developers.cloudflare.com/images/image-resizing/url-format/
		/// </summary>
		public object Options { get; set; }

		/// <summary>
		/// List of Sizes
		/// </summary>
		public string? Sizes { get; set; }

		/// <summary>
		/// Media Query point and image sizes
		/// </summary>
		public List<CloudImageMediaQuery> MediaQueries { get; set; }


		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediaProvider"></param>
        /// <param name="mediaValue"></param>
        /// <param name="umbracoImage"></param>
        /// <param name="imageOrientation"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="quality"></param>
        /// <param name="lazyLoad"></param>
        /// <param name="Options"></param>
        /// <param name="mediaQueries"></param>
        public MediaEmbed(IPublishedContent umbracoImage, int width, int height, int quality, bool lazyLoad, object options, string? sizes, List<CloudImageMediaQuery> mediaQueries)
        {
            this.UmbracoImage = umbracoImage;
            this.Width = width;
            this.Height = height;
            this.Quality = quality;
            this.LazyLoad = lazyLoad;
            this.Options = options;
            this.MediaQueries = mediaQueries;
            this.Sizes = sizes;
        }
    }
}
