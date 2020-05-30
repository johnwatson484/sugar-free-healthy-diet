using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(SugarFreeHealthyDiet.Areas.Identity.IdentityHostingStartup))]
namespace SugarFreeHealthyDiet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IConfiguration Configuration { get; }

        public IdentityHostingStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                var emailConfig = Configuration
                    .GetSection("EmailConfiguration")
                    .Get<SmtpConfiguration>();
                services.AddSingleton(emailConfig);
                services.AddScoped<IEmailSender, SmtpSender>();
            });
        }
    }
}
