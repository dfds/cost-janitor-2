using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.Aggregates;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands.Report
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