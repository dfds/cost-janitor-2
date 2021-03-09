using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands.Report
{
    public sealed class CreateReportCommand : ICommand<ReportRoot>
    {
        [JsonPropertyName("costItems")]
        public IEnumerable<CostItem> CostItems { get; init; }

        [JsonConstructor]
        public CreateReportCommand(IEnumerable<CostItem> costItems)
        {
            CostItems = costItems;
        }
    }
}