using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Domain.Events.Cost
{
    public sealed class CostItemCreatedEvent : CostItemEvent
    {
        public CostItemCreatedEvent(CostItem costItem)
        {
            CostItem = costItem;
        }
    }
}