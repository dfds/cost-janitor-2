using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects
{
    public class AwsGroupDto
    {
        [JsonPropertyName("keys")] 
        public IEnumerable<string> Keys { get; set; }

        [JsonPropertyName("metrics")]
        public IEnumerable<KeyValuePair<string, AwsMetricValueDto>> Metrics { get; set; }
    }
}