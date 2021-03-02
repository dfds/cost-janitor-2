using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CostJanitor.Infrastructure.CostProviders;
using CostJanitor.Infrastructure.CostProviders.Aws;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAmazonWebServices(configuration);
            services.AddKafka(configuration);

            //Library dependencies
            services.AddCostProviders();
        }

        private static void AddCostProviders(this IServiceCollection services)
        {
            services.AddTransient<ICostProvider, AwsCostClient>();
            services.AddTransient<IAwsCostClient, AwsCostClient>();
        }
    }
}
