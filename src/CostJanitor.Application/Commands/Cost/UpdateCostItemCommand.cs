using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.ValueObjects;
using System;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands.Cost
{
    public sealed class UpdateCostItemCommand : ICommand<CostItem>
    {
        [JsonPropertyName("capabilityIdentifier")]
        public string CapabilityIdentifier { get; init; }

        [JsonPropertyName("reportItemId")]
        public Guid ReportItemId { get; init; }

        [JsonPropertyName("label")]
        public string Label { get; init; }

        [JsonPropertyName("value")]
        public string Value { get; init; }

        [JsonConstructor]
        public UpdateCostItemCommand(string capabilityIdentifier, string label, string value, Guid reportItemId)
        {
            CapabilityIdentifier = capabilityIdentifier;
            Label = label;
            Value = value;
            ReportItemId = reportItemId;
        }
    }
}