using AutoMapper;
using Confluent.Kafka;
using CostJanitor.Infrastructure.Kafka.Strategies;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Host.EventConsumer.Strategies
{
    public sealed class AwsAccountEventConsumptionStrategy : ConsumtionStrategy
    {
        public AwsAccountEventConsumptionStrategy(IMapper mapper, IFacade applicationFacade) : base(mapper, applicationFacade)
        {
            
        }

        public override Task Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
        {
            var payload = target.Message.Value;

            if (!string.IsNullOrEmpty(payload))
            {
                var @event = JsonSerializer.Deserialize<IntegrationEvent>(payload);
                var aggregateRoot = _mapper.Map<IAggregateRoot>(@event);
                var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregateRoot);

                if (command != null)
                {
                    _applicationFacade.Execute(command, cancellationToken);
                }
            }

            return Task.CompletedTask;
        }
    }
}
