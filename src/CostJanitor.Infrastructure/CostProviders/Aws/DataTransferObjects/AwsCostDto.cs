using Amazon.CostExplorer.Model;
using System.Collections.Generic;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Model
{
    public class AwsCostDto
    {
        //TODO: Finalize DTO to avoid leaky abstraction
        public IEnumerable<DimensionValuesWithAttributes> DimensionValueAttributes { get; set; }

        public IEnumerable<ResultByTime> ResultsByTime { get; set; }
    }
}