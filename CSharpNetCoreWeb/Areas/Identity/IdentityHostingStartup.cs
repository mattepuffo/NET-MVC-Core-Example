using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CSharpNetCoreWeb.Areas.Identity.IdentityHostingStartup))]
namespace CSharpNetCoreWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}