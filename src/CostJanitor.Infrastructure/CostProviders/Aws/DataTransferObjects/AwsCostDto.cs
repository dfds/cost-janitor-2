using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CostJanitor.Infrastructure.CostProviders.Aws.DataTransferObjects
{
    public class AwsCostDto
    {
        [JsonPropertyName("dimensionValueAttributes")]
        public IEnumerable<AwsDimensionValueAttributeDto> DimensionValueAttributes { get; set; }

        [JsonPropertyName("resultByTime")]
        public IEnumerable<AwsResultByTimeDto> ResultsByTime { get; set; }
    }
}