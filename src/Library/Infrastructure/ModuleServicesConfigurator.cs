using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure
{
    public class ModuleServicesConfigurator : IModuleServicesConfigurator
    {
        public void Configure(IServiceCollection services, IModuleCollection modules, IHostEnvironment env, IConfiguration cfg)
        {
        }
    }
}
