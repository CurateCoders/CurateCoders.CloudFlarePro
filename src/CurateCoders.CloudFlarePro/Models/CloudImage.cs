namespace CurateCoders.CloudFlarePro.Models
{
	public class CloudImage
	{
		/// <summary>
		/// Relative Image Source Url
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// Image Alternative Text
		/// </summary>
		public string Alt { get; set; }

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
		/// List of media query points for responsive images
		/// </summary>
		public List<CloudImageMediaQuery> MediaQueries { get; set; }

		/// <summary>
        /// Overloaded Basic Constructor
        /// </summary>
        /// <param name="mediaImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="quality"></param>
        /// <param name="lazyLoad"></param>
        /// <param name="mediaQueryImages"></param>
        public CloudImage(string imageUrl, string alt, int width, int height, int quality, bool lazyLoad, object options, string? sizes, List<CloudImageMediaQuery> mediaQueries)
        {
            this.ImageUrl = imageUrl;
            this.Alt = alt;
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
