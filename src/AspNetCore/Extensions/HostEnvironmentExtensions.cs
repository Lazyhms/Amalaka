using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.Extensions.Hosting;

public static class HostEnvironmentExtensions
{
    public static IWebHostEnvironment SetBasePath(this IWebHostEnvironment webHostEnvironment, string basePath, string folder = "wwwroot")
    {
        webHostEnvironment.WebRootPath(basePath, folder);
        webHostEnvironment.SetContentRootPath(basePath);

        return webHostEnvironment;
    }

    public static IHostEnvironment SetContentRootPath(this IHostEnvironment hostEnvironment, string basePath)
    {
        hostEnvironment.ContentRootPath = basePath;
        hostEnvironment.ContentRootFileProvider = new PhysicalFileProvider(basePath);
        return hostEnvironment;
    }

    public static IWebHostEnvironment WebRootPath(this IWebHostEnvironment webHostEnvironment, string basePath, string folder = "wwwroot")
    {
        var wwwroot = Path.Combine(basePath, folder);
        if (!Directory.Exists(wwwroot))
        {
            Directory.CreateDirectory(wwwroot);
        }

        webHostEnvironment.WebRootPath = wwwroot;
        webHostEnvironment.WebRootFileProvider = new PhysicalFileProvider(wwwroot);
        return webHostEnvironment;
    }
}
