using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SugarFreeHealthyDiet.Areas.Identity.IdentityHostingStartup))]
namespace SugarFreeHealthyDiet.Areas.Identity
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
