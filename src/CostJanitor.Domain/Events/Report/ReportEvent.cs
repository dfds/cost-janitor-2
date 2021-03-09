using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;

namespace CostJanitor.Domain.Events.Report
{
    public abstract class ReportEvent : IDomainEvent
    {
        public ReportRoot ReportItem { get; protected set; }
    }
}