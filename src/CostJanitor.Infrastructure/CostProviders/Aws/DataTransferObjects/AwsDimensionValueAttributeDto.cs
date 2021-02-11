using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects
{
    public class AwsDimensionValueAttributeDto
    {
        [JsonPropertyName("attributes")]
        public IEnumerable<KeyValuePair<string, string>> Attributes { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}