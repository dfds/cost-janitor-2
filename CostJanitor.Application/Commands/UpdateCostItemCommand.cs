using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class UpdateCostItemCommand : ICommand<CostItem>
    {
        [JsonPropertyName("capabilityIdentifier")]
        public string CapabilityIdentifier { get; init; }
        [JsonPropertyName("label")]
        public string Label { get; init; }
        [JsonPropertyName("value")]
        public string Value { get; init; }

        [JsonConstructor]
        public UpdateCostItemCommand(string id, string label, string value)
        {
            CapabilityIdentifier = id;
            Label = label;
            Value = value;
        }
    }
}