using AutoMapper;
using CloudEngineering.CodeOps.Infrastructure.Kafka;
using CostJanitor.Application;
using CostJanitor.Host.EventConsumer.Strategies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Host.EventConsumer
{
    public class AwsAccountEventWorker : KafkaConsumerService
    {
        public AwsAccountEventWorker(ILogger<KafkaConsumerService> logger, IOptions<KafkaOptions> options, IMapper mapper, IApplicationFacade applicationFacade) : base(logger, options, new AwsAccountEventConsumptionStrategy(mapper, applicationFacade))
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting: {0}", nameof(AwsAccountEventWorker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping: {0}", nameof(AwsAccountEventWorker));

            return base.StopAsync(cancellationToken);
        }
    }
}
