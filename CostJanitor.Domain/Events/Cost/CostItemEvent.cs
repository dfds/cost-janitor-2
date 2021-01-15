using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Domain.Events.Cost
{
    public abstract class CostItemEvent : IDomainEvent
    {
        public CostItem CostItem { get; protected set; }
        
    }
}