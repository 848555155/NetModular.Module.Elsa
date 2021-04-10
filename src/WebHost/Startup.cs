using Elsa.Activities.Http.Extensions;
using Elsa.Dashboard.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Host.Web;
using NetModular.Module.Elsa.Application.Extensions;

namespace NetModular.Module.Elsa.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostEnvironment env, IConfiguration cfg) : base(env, cfg)
        {
        }

        public override void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            base.Configure(app, appLifetime);
            app.UseStaticFiles()
                .UseHttpActivities();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services
                .AddElsaDashboard()
                .AddHttpActivities();
        }
    }
}
