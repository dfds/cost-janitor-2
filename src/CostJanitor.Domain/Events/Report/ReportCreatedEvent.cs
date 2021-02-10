using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Domain.Events.Report
{
    public sealed class ReportCreatedEvent : ReportEvent
    {
        public ReportCreatedEvent(ReportRoot reportItem)
        {
            ReportItem = reportItem;
        }
    }
}