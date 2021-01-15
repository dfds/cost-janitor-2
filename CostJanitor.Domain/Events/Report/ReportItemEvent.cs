using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Domain.Events.Report
{
    public abstract class ReportItemEvent : IDomainEvent
    {
        public ReportItem ReportItem { get; protected set; }
    }
}