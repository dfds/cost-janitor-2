using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class CreateReportCommand : ICommand<ReportItem>
    {
        [JsonPropertyName("reportId")]
        public Guid ReportId { get; init; }
        [JsonPropertyName("costItems")]
        public IEnumerable<CostItem> CostItems { get; init; }

        [JsonConstructor]
        public CreateReportCommand(Guid reportId, IEnumerable<CostItem> costItems)
        {
            ReportId = reportId;
            CostItems = costItems;
        }
    }
}