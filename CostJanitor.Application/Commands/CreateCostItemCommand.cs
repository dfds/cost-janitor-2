using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class CreateCostItemCommand : ICommand<CostItem>
    {
        [JsonPropertyName("costItemId")]
        public Guid CostItemId { get; init; }
        [JsonPropertyName("label")]
        public string Label { get; init; }
        [JsonPropertyName("value")]
        public string Value { get; init; }

        [JsonConstructor]
        public CreateCostItemCommand(Guid id, string label, string value)
        {
            CostItemId = id;
            Label = label;
            Value = value;
        }
    }
}