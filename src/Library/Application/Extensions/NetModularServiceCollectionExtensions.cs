using Elsa;
using Elsa.Extensions;
using Elsa.Mapping;
using Elsa.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Module.Elsa.Application.Services;

namespace NetModular.Module.Elsa.Application.Extensions
{
    public static class NetModularServiceCollectionExtensions
    {
        public static ElsaBuilder AddNetModularStores(
            this ElsaBuilder configuration)
        {
            return configuration
                .AddWorkflowDefinitionStore()
                .AddWorkflowInstanceStore();
        }

        public static ElsaBuilder AddWorkflowInstanceStore(this ElsaBuilder configuration)
        {
            configuration.Services
                .AddScoped<IWorkflowInstanceStore, NetModularWorkflowInstanceStrore>()
                .AddMapperProfile<NodaTimeProfile>(ServiceLifetime.Singleton)
                .AddMapperProfile<EntitiesProfile>(ServiceLifetime.Singleton);

            return configuration;
        }

        public static ElsaBuilder AddWorkflowDefinitionStore(this ElsaBuilder configuration)
        {
            configuration.Services
                .AddScoped<IWorkflowDefinitionStore, NetModularWorkflowDefinitionStore>();

            return configuration;
        }
    }
}
