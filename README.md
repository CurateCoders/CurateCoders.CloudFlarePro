# Curate Coders' Cloudflare Pro helper package

![Nuget](https://img.shields.io/nuget/dt/CurateCoders.CloudflarePro)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/CurateCoders/CurateCoders.CloudflarePro/dotnet.yml)

This package provides helper classes & views to leverage Cloudflare Pro's Image Resizing service for providing responsive images using Umbraco's media cropping facilities.

To get started, install the package via Nuget:

- Powershell

	``` Install-Package CurateCoders.CloudflarePro ```

- dotnet CLI

	``` dotnet add package CurateCoders.CloudFlarePro ```

- Please note! We have updated the package to introduce a far simpler way of using the package, see example 2 below but both approaches will continue to work for backwards compatibility.

### Example 1 (Object Oriented Approach)

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
- ```cssClasses``` - a nullable list of ```Css Classes``` space separated list of css classes, these will be specific to your project but will be useful for any styling tweaks if required.

In progress, in the meantime please check ```Views\_ExampleView.cshtml``` for some sample usage.

If using the sample code as-is, **please** change the URL on line 47 of ```Views\Shared\_ExamplePartialView.cshtml``` to be the URL of your Cloudflare account!


### Example 2 (New simple declarative approach) - Updated 11/2025

Once you have installed the package, you'll find an example Razor view in ```_ExampleViewNew.cshtml```.

The new ```CloudflareOptions``` object is passed to the HtmlHelper ```GetCloudflareImageTag```, which has the following properties:
- ```CdnZoneUrl``` - your media cdn folder, the root sub domain of your cdn mapped to your storage account;
- ```Src``` - the Image.MediaUrl() src url of your image
- ```Alt``` - the alt text meta tag for your image
- ```Width``` - default width of the primary image;
- ```Height``` - default height of the primary image;
- ```Quality``` - quality of the images to be generated based on the original (for WebP/JPEG/PNG etc.);
- ```LazyLoad``` - whether responsive images should be lazily loaded;
- ```Options``` - Cloudflare Pro-specific options as a querystring entries upon rendering; - see [here](https://developers.cloudflare.com/images/transform-images/transform-via-url/) for full documentation.
- ```Sizes``` - A CSS rule to dictate at which sizes Cloudflare Pro Image Resizing should be invoked - see [here](https://developers.cloudflare.com/images/image-resizing/responsive-images/#the-sizes-attribute) for full documentation on the feature;
- ```SrcSetSpec``` - a comma separated string of ':' separated src specs, which are represented in the format ```"key:width:height"``` - the key must end in 'w' eg 400w or 500w - Please check ```Views\_ExampleViewNew.cshtml``` for some sample usage.
- ```cssClasses``` - a nullable list of ```Css Classes``` space separated list of css classes, these will be specific to your project but will be useful for any styling tweaks if required.

Please check ```Views\_ExampleViewNew.cshtml``` for some sample usage.
Please note you must change line 19 to be the url of your cdn including its sub domain. https://cdn.yourdomain.com/