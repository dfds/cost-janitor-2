using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.Commands.Cost;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.Factories;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CloudEngineering.CodeOps.Infrastructure.Kafka.Serialization;
using Confluent.Kafka;
using CostJanitor.Infrastructure.CostProviders.Aws;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection;

namespace CostJanitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediator();
            services.AddAws(configuration);
            services.AddEntityFramework(configuration);
            services.AddKafka(configuration);
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddTransient<ServiceFactory>(p => p.GetService);

            services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
        }

        private static void AddAws(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AwsFacade).Assembly);

            services.Configure<AwsFacadeOptions>(configuration.GetSection(AwsFacadeOptions.AwsFacade));

            services.AddTransient<IRequestHandler<GetMonthlyTotalCostCommand, IEnumerable<CostDto>>, GetMonthlyTotalCostCommandHandler>();
            services.AddTransient<ICommandHandler<GetMonthlyTotalCostCommand, IEnumerable<CostDto>>, GetMonthlyTotalCostCommandHandler>();
            services.AddTransient<IAwsClientFactory, AwsClientFactory>();
            services.AddTransient<IAwsFacade, AwsFacade>();
            services.AddTransient<IAwsCostClient, AwsCostClient>();
        }

        private static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Kafka));

            services.AddTransient(p =>
            {
                var logger = p.GetService<ILogger<IProducer<string, IIntegrationEvent>>>();
                var producerOptions = p.GetService<IOptions<KafkaOptions>>();
                var producerBuilder = new ProducerBuilder<string, IIntegrationEvent>(producerOptions.Value.Configuration);
                var producer = producerBuilder.SetErrorHandler((_, e) => logger.LogError($"Error: {e?.Reason}", e))
                                            .SetStatisticsHandler((_, json) => logger.LogDebug($"Statistics: {json}"))
                                            .SetValueSerializer(new DefaultValueSerializer<IIntegrationEvent>())
                                            .Build();

                return producer;
            });
        }

        private static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EntityContextOptions>(configuration);
        }
    }
}
