using AutoMapper;
using Confluent.Kafka;
using CloudEngineering.CodeOps.Abstractions.Events;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Events.Report
{
    public class ReportCreatedIntegrationEventHandler : IEventHandler<ReportCreatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IProducer<Ignore, IIntegrationEvent> _producer;
        
        public ReportCreatedIntegrationEventHandler(IMapper mapper, IProducer<Ignore, IIntegrationEvent> producer = default)
        {
            _mapper = mapper;
            _producer = producer;
        }
        
        public Task Handle(ReportCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}