using System.Text.Json.Serialization;

namespace CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects
{
    public class AwsMetricValueDto
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }
}