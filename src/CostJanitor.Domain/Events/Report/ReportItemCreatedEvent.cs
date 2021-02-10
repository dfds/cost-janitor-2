using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Domain.Events.Report
{
    public sealed class ReportItemCreatedEvent : ReportItemEvent
    {
        public ReportItemCreatedEvent(ReportItem reportItem)
        {
            ReportItem = reportItem;
        }
    }
}