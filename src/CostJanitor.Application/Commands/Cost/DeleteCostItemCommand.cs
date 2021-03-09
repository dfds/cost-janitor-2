using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands.Cost
{
    public sealed class DeleteCostItemCommand : ICommand<bool>
    {
        [JsonPropertyName("capabilityIdentifier")]
        public string CapabilityIdentifier { get; init; }

        [JsonPropertyName("label")]
        public string Label { get; init; }

        [JsonPropertyName("reportItemId")]
        public Guid ReportItemId { get; init; }

        [JsonConstructor]
        public DeleteCostItemCommand(Guid reportItemId, string label, string capabilityIdentifier = default)
        {
            Label = label;
            CapabilityIdentifier = capabilityIdentifier;
            ReportItemId = reportItemId;
        }
    }
}