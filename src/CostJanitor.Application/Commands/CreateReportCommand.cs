using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Commands;

namespace CostJanitor.Application.Commands
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