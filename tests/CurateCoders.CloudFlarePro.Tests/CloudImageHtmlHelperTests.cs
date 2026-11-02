using CurateCoders.CloudFlarePro.Helpers;
using CurateCoders.CloudFlarePro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xunit;

namespace CurateCoders.CloudFlarePro.Tests;

public class CloudImageHtmlHelperTests
{
    private const string Cdn = "https://cdn.example.com/";

    private static string Render(CloudflareImageOptions o)
    {
        IHtmlHelper helper = null!;
        using var w = new StringWriter();
        helper.GetCloudflareImageTag(o).WriteTo(w, System.Text.Encodings.Web.HtmlEncoder.Default);
        return w.ToString();
    }

    private static CloudflareImageOptions Opts() => new()
    {
        CdnZoneUrl = Cdn, Src = Cdn + "media/a.jpg", Alt = "A \"cat\"", Width = "400", Height = "300",
        Quality = "80", LazyLoad = true, SrcSetSpec = "400w:400:300,800w:800:600", Sizes = "(max-width: 600px) 400px, 800px", CssClasses = "img-fluid"
    };

    [Fact]
    public void CdnUrl_WithoutOptions()
    {
        var img = new CloudflareImage(Cdn + "media/a.jpg", "a", "400", "300", "80", true, null, null, null);
        Assert.Equal(Cdn + "cdn-cgi/image/width=400,height=300,quality=80/media/a.jpg", CloudImageHtmlHelper.GetCloudflareImageCdnUrl(Cdn, img, "400", "300"));
    }

    [Fact]
    public void CdnUrl_WithOptions()
    {
        var img = new CloudflareImage(Cdn + "media/a.jpg", "a", "400", "300", "80", true, "fit=cover", null, null);
        Assert.Equal(Cdn + "cdn-cgi/image/fit=cover,width=400,height=300,quality=80/media/a.jpg", CloudImageHtmlHelper.GetCloudflareImageCdnUrl(Cdn, img, "400", "300"));
    }

    [Fact]
    public void Tag_Lazy_RendersSrcsetAndAttributes()
    {
        var html = Render(Opts());
        Assert.Contains("loading=\"lazy\"", html);
        Assert.DoesNotContain("fetchpriority", html);
        Assert.Contains("width=800,height=600,quality=80/media/a.jpg  800w", html);
        Assert.Contains("sizes=\"", html);
        Assert.Contains("class=\"img-fluid\"", html);
        Assert.Contains("alt=\"A &quot;cat&quot;\"", html);
    }

    [Fact]
    public void Tag_NotLazy_HasFetchPriority()
    {
        var o = Opts(); o.LazyLoad = false;
        var html = Render(o);
        Assert.Contains("fetchpriority=\"high\"", html);
        Assert.DoesNotContain("loading=\"lazy\"", html);
    }

    [Fact]
    public void LegacyTag_RendersOptionsFromObject()
    {
        var ci = new CloudImage(Cdn + "media/a.jpg", "a", 400, 300, 80, true, new { fit = "cover" }, null, new List<CloudImageMediaQuery>());
        Assert.Equal(Cdn + "cdn-cgi/image/fit=cover,width=400,height=300,quality=80/media/a.jpg", CloudImageHtmlHelper.GetImageCdnUrl(Cdn, ci, 400, 300));
    }
}
