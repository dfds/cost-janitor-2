using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Converters;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CloudEngineering.CodeOps.Infrastructure.AzureDevOps.UnitTest.Mapping.Converters
{
    public class CostDtoToReportRootConverterTests
    {
        [Fact]
        public void CanConvertAllAccountsPayload()
        {
            //Arrange
            var fakeCostDto = new CostDto()
            {
                DimensionValueAttributes = new[] { new DimensionValueAttributeDto() { Attributes = new[] { new KeyValuePair<string, string>("description", "dfds-AWS_ACCOUNT_NAME") }, Value = "AWS_ACCOUNT_ID" } },
                ResultsByTime = new[] 
                { 
                    new ResultByTimeDto() 
                    { 
                        Groups = new[] 
                        { 
                            new GroupDto() { Keys = new[] { "AWS_ACCOUNT_ID" }, Metrics = new[] { new KeyValuePair<string, MetricValueDto>("BlendedCost", new MetricValueDto() { Amount = "100", Unit = "USD" }) } },
                            new GroupDto() { Keys = new[] { "AWS_ACCOUNT_ID" }, Metrics = new[] { new KeyValuePair<string, MetricValueDto>("BlendedCost", new MetricValueDto() { Amount = "200", Unit = "DKK" }) } }
                        } 
                    } 
                }
            };

            var sut = new CostDtoToReportRootConverter();

            //Act
            var result = sut.Convert(fakeCostDto, null, null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(result is ReportRoot);
            Assert.Equal("AWS_ACCOUNT_NAME", result.CostItems.First().CapabilityIdentifier);
            Assert.Equal("monthlyTotalCost", result.CostItems.First().Label);
            Assert.Equal("300", result.CostItems.First().Value);
        }

        [Fact]
        public void CanConvertSingleAccountPayload()
        {
            //Arrange
            var fakeCostDto = new CostDto()
            {
                ResultsByTime = new[]
                {
                    new ResultByTimeDto()
                    {
                        Total = new[] 
                        { 
                            new KeyValuePair<string, MetricValueDto>("BlendedCost", new MetricValueDto() { Amount = "100", Unit = "USD" }), 
                            new KeyValuePair<string, MetricValueDto>("BlendedCost", new MetricValueDto() { Amount = "200", Unit = "DKK" }) 
                        }
                    }
                }
            };

            var sut = new CostDtoToReportRootConverter();

            //Act
            var result = sut.Convert(fakeCostDto, null, null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(result is ReportRoot);
            Assert.Equal("monthlyTotalCost", result.CostItems.First().Label);
            Assert.Equal("300", result.CostItems.First().Value);
        }
    }
}
