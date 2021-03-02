using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CloudEngineering.CodeOps.Security.Policies;
using CostJanitor.Infrastructure.CostProviders;
using CostJanitor.Infrastructure.CostProviders.Aws;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CostJanitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //External dependencies
            services.AddTransient<ServiceFactory>(p => p.GetService);
            services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
            services.AddAmazonWebServices(configuration);
            services.AddKafka(configuration);
            services.AddSecurityPolicies();

            //Custom dependencies
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCostProviders();
        }

        private static void AddCostProviders(this IServiceCollection services)
        {
            services.AddTransient<ICostProvider, AwsCostClient>();
            services.AddTransient<IAwsCostClient, AwsCostClient>();
        }
    }
}
