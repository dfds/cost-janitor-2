using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Events;

namespace CostJanitor.Domain.Events.Report
{
    public abstract class ReportEvent : IDomainEvent
    {
        public ReportRoot ReportItem { get; protected set; }
    }
}