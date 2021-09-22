using AutoMapper;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters
{
    public class CostDtoToReportRootConverter : ITypeConverter<CostDto, ReportRoot>
    {
        private readonly string _decimalSeperator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public ReportRoot Convert(CostDto source, ReportRoot destination, ResolutionContext context)
        {
            destination ??= new ReportRoot();

            var costItems = new List<CostItem>();
            IEnumerable<IGrouping<string, KeyValuePair<string, MetricValueDto>>> blendCostMetricUnitGroups = Enumerable.Empty<IGrouping<string, KeyValuePair<string, MetricValueDto>>>();

            if (source.DimensionValueAttributes != null && source.DimensionValueAttributes.Any())
            {
                foreach (var dimensionValueAttribute in source.DimensionValueAttributes)
                {
                    var awsAccountName = dimensionValueAttribute.Attributes.Single(o => o.Key == "description").Value;
                    var awsAccountId = dimensionValueAttribute.Value;

                    foreach (var resultByTime in source.ResultsByTime.Where(o => o.Groups.Any(g => g.Keys.Any(k => k == awsAccountId))))
                    {
                        blendCostMetricUnitGroups = resultByTime.Groups.Where(g => g.Keys.Any(k => k == awsAccountId)).SelectMany(g => g.Metrics.Where(o => o.Key == "BlendedCost")).GroupBy(m => m.Value.Unit);
                    }
                }
            }
            else
            {
                blendCostMetricUnitGroups = source.ResultsByTime.FirstOrDefault().Total.Where(m => m.Key == "BlendedCost").GroupBy(m => m.Value.Unit);                
            }

            var totalCost = 0m;

            foreach (var metricUnitGroup in blendCostMetricUnitGroups)
            {
                totalCost += CalculateMetricUnitGroupCost(metricUnitGroup);
            }

            var costItem = new CostItem("monthlyTotalCost", totalCost.ToString(), context.Items["CapabilityId"].ToString());

            costItems.Add(costItem);
            destination.AddCostItem(costItems);

            return destination;
        }

        private decimal CalculateMetricUnitGroupCost(IGrouping<string, KeyValuePair<string, MetricValueDto>> metricUnitGroup)
        {
            var totalCost = 0m;

            foreach (var metric in metricUnitGroup)
            {
                var metricAmount = metric.Value.Amount;

                if (!metricAmount.Contains(_decimalSeperator))
                {
                    metricAmount = metricAmount.Replace(".", _decimalSeperator);
                }

                if (decimal.TryParse(metricAmount, out var parsedValue))
                {
                    totalCost += parsedValue;
                }
            }

            return totalCost;
        }
    }
}