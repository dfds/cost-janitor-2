using CostJanitor.Infrastructure.CostProviders.Aws;
using CostJanitor.Infrastructure.IntegrationTest.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Infrastructure.IntegrationTest.CostProviders.Aws
{
    public class AwsCostClientTests : IClassFixture<AwsFacadeFixture>
    {
        private readonly AwsFacadeFixture _awsFixture;

        public AwsCostClientTests(AwsFacadeFixture fixture)
        {
            _awsFixture = fixture;
        }

        [Fact]
        public async Task GetMonthlyTotalCostAllAccountsTest()
        {
            using var facade = _awsFixture.Facade;
            var sut = new AwsCostClient(facade);
            var resp = await sut.GetMonthlyTotalCostAllAccountsAsync();

            Assert.NotNull(resp);

            foreach (var result in resp)
            {
                Assert.NotEmpty(result.ResultsByTime);
                Assert.NotEmpty(result.DimensionValueAttributes);
            }
        }

        [Fact(Skip = "Fix this test, figure out why its mapping incorrectly")]
        public async Task GetMonthlyTotalCostsByAccountId()
        {
            using var facade = _awsFixture.Facade;
            var sut = new AwsCostClient(facade);
            var resp = await sut.GetMonthlyTotalCostByAccountIdAsync("642375522597");

            Assert.NotEmpty(resp.ResultsByTime);
            Assert.NotEmpty(resp.DimensionValueAttributes);
        }
    }
}