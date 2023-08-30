using Umbraco.Cms.Core.Models.PublishedContent;

namespace CurateCoders.CloudFlarePro.Models
{
	public class MediaEmbed
	{
		/// <summary>
		/// Media Provider
		/// </summary>
		public string MediaProvider { get; set; }

		/// <summary>
		/// Media Value ID
		/// </summary>
		public string MediaValue { get; set; }

		/// <summary>
		/// UmbracoImage reference as IPublishedContent
		/// </summary>
		public IPublishedContent UmbracoImage { get; set; }

		/// <summary>
		/// Media Image Orientation
		/// </summary>
		public string ImageOrientation { get; set; }

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
        public MediaEmbed(string mediaProvider, string mediaValue, IPublishedContent umbracoImage, string imageOrientation, int width, int height, int quality, bool lazyLoad, object Options, string? sizes, List<CloudImageMediaQuery> mediaQueries)
        {
            this.MediaProvider = mediaProvider;
            this.MediaValue = mediaValue;
            this.UmbracoImage = umbracoImage;
            this.ImageOrientation = imageOrientation;
            this.Width = width;
            this.Height = height;
            this.Quality = quality;
            this.LazyLoad = lazyLoad;
            this.Options = Options;
            this.MediaQueries = mediaQueries;
            this.Sizes = sizes;
        }
    }
}
