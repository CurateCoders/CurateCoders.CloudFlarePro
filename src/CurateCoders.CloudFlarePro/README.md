# Curate Coders' Cloudflare Pro helper package

![Nuget](https://img.shields.io/nuget/dt/CurateCoders.CloudflarePro)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/CurateCoders/CurateCoders.CloudflarePro/dotnet.yml)

This package provides helper classes & views to leverage Cloudflare Pro's Image Resizing service for providing responsive images using Umbraco's media cropping facilities.

To get started, install the package via Nuget:

- Powershell

	``` Install-Package CurateCoders.CloudflarePro ```

- dotnet CLI

	``` dotnet add package CurateCoders.CloudFlarePro ```

Once you have installed the package, you'll find an example Razor view in ```_ExampleView.cshtml```.

The main parameter passed to the partial is an instance of the ```MediaEmbed``` class, which has the following parameters:
- ```umbracoImage``` - the Umbraco image (an ```IPublishedContent``` object) you wish to generate sizes for;
- ```width``` - default width of the primary image;
- ```height``` - default height of the primary image;
- ```quality``` - quality of the images to be generated based on the original (for WebP/JPEG/PNG etc.);
- ```lazyLoad``` - whether responsive images should be lazily loaded;
- ```options``` - Cloudflare Pro-specific options as a a POCO which then gets converted into additional querystring entries upon rendering;
- ```sizes``` - A CSS rule to dictate at which sizes Cloudflare Pro Image Resizing should be invoked - see [here](https://developers.cloudflare.com/images/image-resizing/responsive-images/#the-sizes-attribute) for full documentation on the feature;
- ```mediaQueries``` - a list of ```CloudImageMediaQuery``` objects, which specify at which screen size breakpoints/viewport widths you want Cloudflare Image Resizing to generate URLs for.

### Examples

In progress, in the meantime please check ```Views\_ExampleView.cshtml``` for some sample usage.

If using the sample code as-is, **please** change the URL on line 47 of ```Views\Shared\_ExamplePartialView.cshtml``` to be the URL of your Cloudflare account!