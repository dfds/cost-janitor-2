using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Report;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Application.Events.Report
{
    public sealed class ReportItemCreatedHandler : IEventHandler<ReportItemCreatedEvent>
    {
        private readonly IMapper _mapper;
        
        
        public ReportItemCreatedHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Handle(ReportItemCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}