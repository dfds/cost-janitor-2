using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class DeleteCostItemCommand : ICommand<bool>
    {
        [JsonPropertyName("costItemId")]
        public Guid CostItemId { get; init; }
        [JsonPropertyName("reportItemId")]
        public Guid ReportItemId { get; init; }

        [JsonConstructor]
        public DeleteCostItemCommand(Guid costItemId, Guid reportItemId)
        {
            CostItemId = costItemId;
            ReportItemId = reportItemId;
        }

        public DeleteCostItemCommand()
        {
            
        }
    }
}