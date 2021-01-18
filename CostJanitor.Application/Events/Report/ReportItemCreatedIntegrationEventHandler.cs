using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using MediatR;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Application.Events.Report
{
    public class ReportItemCreatedIntegrationEventHandler : IEventHandler<ReportItemCreatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        private readonly IProducer<Ignore, IIntegrationEvent> _producer;
        
        public ReportItemCreatedIntegrationEventHandler(IMapper mapper, IProducer<Ignore, IIntegrationEvent> producer = default)
        {
            _mapper = mapper;
            _producer = producer;
        }
        
        public Task Handle(ReportItemCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}