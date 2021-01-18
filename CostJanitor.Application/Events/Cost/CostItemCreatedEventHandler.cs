using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CostJanitor.Domain.Events;
using CostJanitor.Domain.Events.Cost;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Application.Events.Cost
{
    public sealed class CostItemCreatedEventHandler : IEventHandler<CostItemCreatedEvent>
    {
        private readonly IMapper _mapper;

        public CostItemCreatedEventHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Handle(CostItemCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}