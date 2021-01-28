using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Domain.Events.Cost
{
    public sealed class CostItemIdChangedEvent : CostItemEvent
    {
        public CostItemIdChangedEvent(CostItem costItem)
        {
            CostItem = costItem;
        }
    }
}