using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurateCoders.CloudFlarePro.Models
{
    public class CloudflareImageOptions
    {
        public string? CdnZoneUrl { get; set; }
        public string? Src { get; set; }
        public string? Alt { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? Quality { get; set; }
        public bool LazyLoad { get; set; }
        public string? Options { get; set; }
        public string? Sizes { get; set; }
        public string? SrcSetSpec { get; set; }
        public string? CssClasses { get; set; }
    }
}
