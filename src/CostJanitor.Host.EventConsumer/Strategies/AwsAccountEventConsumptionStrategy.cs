using AutoMapper;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Events;
using CloudEngineering.CodeOps.Infrastructure.Kafka.Strategies;
using Confluent.Kafka;
using CostJanitor.Application;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Host.EventConsumer.Strategies
{
    public sealed class AwsAccountEventConsumptionStrategy : ConsumtionStrategy
    {
        public AwsAccountEventConsumptionStrategy(IMapper mapper, IApplicationFacade applicationFacade) : base(mapper, applicationFacade)
        {
            
        }

        public override async Task Apply(ConsumeResult<string, string> target, CancellationToken cancellationToken)
        {
            var payload = target.Message.Value;

            if (!string.IsNullOrEmpty(payload))
            {
                payload = payload.Replace("x-", "")
                                 .Replace("\"eventName\"", "\"type\"")
                                 .Replace("\"version\"", "\"schemaVersion\"");

                var @event = JsonSerializer.Deserialize<IntegrationEvent>(payload);
                var aggregateRoot = _mapper.Map<IAggregateRoot>(@event);
                var command = _mapper.Map<IAggregateRoot, ICommand<IAggregateRoot>>(aggregateRoot);

                if (command != null)
                {
                    await _applicationFacade.Execute(command, cancellationToken);
                }
            }

            return;
        }
    }
}
