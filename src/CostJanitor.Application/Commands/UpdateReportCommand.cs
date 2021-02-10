using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands
{
    public sealed class UpdateReportCommand : ICommand<ReportRoot>
    {
        [JsonPropertyName("report")]
        public ReportRoot Report { get; init; }

        [JsonConstructor]
        public UpdateReportCommand(ReportRoot report)
        {
            Report = report;
        }
    }
}