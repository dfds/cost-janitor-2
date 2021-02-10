using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
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