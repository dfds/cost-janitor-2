using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Domain.Events
{
    public class CostItemCreatedEvent : IDomainEvent
    {
        public CostItem CostItem { get; init; }

        public CostItemCreatedEvent(CostItem costItem)
        {
            this.CostItem = costItem;
        }
    }
}