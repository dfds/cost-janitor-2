using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.ValueObjects
{
    public sealed class CostItem : ValueObject
    {
        [Required]
        [JsonPropertyName("label")]
        public string Label { get; init; }
        [Required]
        [JsonPropertyName("value")]
        public string Value { get; init; }
        [Required]
        [JsonPropertyName("capabilityIdentifier")]
        public string CapabilityIdentifier { get; init; }
        
        [JsonConstructor]
        public CostItem(string label, string value, string capabilityIdentifier)
        {
            this.Label = label;
            this.Value = value;
            this.CapabilityIdentifier = capabilityIdentifier;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}