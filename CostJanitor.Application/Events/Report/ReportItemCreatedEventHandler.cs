using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Report;
using MediatR;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Application.Events.Report
{
    public sealed class ReportItemCreatedEventHandler : IEventHandler<ReportItemCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        
        public ReportItemCreatedEventHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(ReportItemCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(_mapper.Map<ReportItemCreatedIntegrationEvent>(@event), cancellationToken);
        }
    }
}