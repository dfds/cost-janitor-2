using CloudEngineering.CodeOps.Infrastructure.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Host.EventConsumer
{
    public class AwsAccountEventWorker : KafkaConsumerService
    {
        public AwsAccountEventWorker(IOptions<KafkaOptions> options, IServiceScopeFactory scopeFactory) : base(options, scopeFactory)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation("Starting: {0}", nameof(AwsAccountEventWorker));

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation("Stopping: {0}", nameof(AwsAccountEventWorker));

            return base.StopAsync(cancellationToken);
        }
    }
}
