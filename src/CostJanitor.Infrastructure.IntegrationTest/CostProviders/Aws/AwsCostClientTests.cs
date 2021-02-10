using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.CostExplorer;
using CostJanitor.Infrastructure.CostProviders.Aws;
using Xunit;

namespace CostJanitor.Infrastructure.IntegrationTest.CostProviders.Aws
{
    // This assumes a valid AWS session with the right permissions (CostExplorer access)
    public class AwsCostClientTests
    {
        [Fact]
        public async Task GetMonthlyTotalCostAllAccountsTest()
        {
            IAwsCostClient sut = new AwsCostClient(new AmazonCostExplorerClient(RegionEndpoint.USEast1));
            var resp = await sut.GetMonthlyTotalCostAllAccounts();
            
            Assert.NotNull(resp);

            foreach (var result in resp)
            {
                Assert.Equal(HttpStatusCode.OK, result.HttpStatusCode);
                Assert.NotEmpty(result.ResultsByTime);
                Assert.NotEmpty(result.DimensionValueAttributes);
            }
        }

        [Fact]
        public async Task GetMonthlyTotalCostsByAccountId()
        {
            IAwsCostClient sut = new AwsCostClient(new AmazonCostExplorerClient(RegionEndpoint.USEast1));
            var resp = await sut.GetMonthlyTotalCostByAccountId("642375522597");
            
            Assert.Equal(HttpStatusCode.OK, resp.HttpStatusCode);
            Assert.NotEmpty(resp.ResultsByTime);
            Assert.NotEmpty(resp.DimensionValueAttributes);
        }
    }
}