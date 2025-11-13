namespace CurateCoders.CloudFlarePro.Models
{
	public class CloudflareImage
    {
		/// <summary>
		/// Relative Image Source Url
		/// </summary>
		public string? Src { get; set; }

		/// <summary>
		/// Image Alternative Text
		/// </summary>
		public string? Alt { get; set; }

		/// <summary>
		/// Mobile / default image width
		/// </summary>
		public string? Width { get; set; }

		/// <summary>
		/// Mobile / default image height
		/// </summary>
		public string? Height { get; set; }

		/// <summary>
		/// Image compression quality
		/// </summary>
		public string? Quality { get; set; }

		/// <summary>
		/// LazyLoad image
		/// </summary>
		public bool LazyLoad { get; set; }

        /// <summary>
        /// List of Options: https://developers.cloudflare.com/images/transform-images/transform-via-url/
        /// </summary>
        public string? Options { get; set; }

        /// <summary>
        /// List of Sizes
        /// </summary>
        public string? Sizes { get; set; }

		/// <summary>
		/// List of media query points for responsive images
		/// </summary>
		public string? SrcSetSpec { get; set; }

        /// <summary>
        /// List of Css Classes
        /// </summary>
        public string? CssClasses { get; set; }


        /// <summary>
        /// Overloaded Basic Constructor with CssClasses
        /// </summary>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="quality"></param>
        /// <param name="lazyLoad"></param>
        /// <param name="options"></param>
        /// <param name="sizes"></param>
        /// <param name="srcSetSpec"></param>
        /// <param name="cssClasses"></param>
        public CloudflareImage(string? src, string? alt, string? width, string? height, string? quality, bool lazyLoad, string? options, string? sizes, string? srcSetSpec, string? cssClasses)
        {
            this.Src = src;
            this.Alt = alt;
            this.Width = width;
            this.Height = height;
            this.Quality = quality;
            this.LazyLoad = lazyLoad;
            this.Options = options;
            this.Sizes = sizes;
            this.SrcSetSpec = srcSetSpec;
            this.CssClasses = cssClasses;
        }

        /// <summary>
        /// Overloaded Basic Constructor
        /// </summary>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="quality"></param>
        /// <param name="lazyLoad"></param>
        /// <param name="options"></param>
        /// <param name="sizes"></param>
        /// <param name="srcSetSpec"></param>
        public CloudflareImage(string? src, string? alt, string? width, string? height, string? quality, bool lazyLoad, string? options, string? sizes, string? srcSetSpec)
        {
            this.Src = src;
            this.Alt = alt;
            this.Width = width;
            this.Height = height;
            this.Quality = quality;
            this.LazyLoad = lazyLoad;
            this.Options = options;
            this.Sizes = sizes;
            this.SrcSetSpec = srcSetSpec;
        }
    }
}
