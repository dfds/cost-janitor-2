using AutoMapper;
using CostJanitor.Host.EventConsumer.Strategies;
using CostJanitor.Infrastructure.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResourceProvisioning.Abstractions.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Host.EventConsumer
{
    public class AwsAccountEventWorker : KafkaConsumerService
    {
        public AwsAccountEventWorker(ILogger<KafkaConsumerService> logger, IOptions<KafkaOptions> options, IMapper mapper, IFacade applicationFacade) : base(logger, options, new AwsAccountEventConsumptionStrategy(mapper, applicationFacade))
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
