namespace CurateCoders.CloudFlarePro.Models
{
	public class CloudImageMediaQuery
	{
		/// <summary>
		/// Media querey break point name
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Width in pixels
		/// </summary>
		public string Width { get; set; }

		/// <summary>
		/// Height in pixels
		/// </summary>
		public string Height { get; set; }

		/// <summary>
		/// Default Constructor
		/// </summary>
		public CloudImageMediaQuery()
		{ }

		/// <summary>
		/// Anonymous Constructor
		/// </summary>
		/// <param name="key"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public CloudImageMediaQuery(string key, string width, string height)
		{
			this.Key = key;
			this.Width = width;
			this.Height = height;
		}
	}
}
