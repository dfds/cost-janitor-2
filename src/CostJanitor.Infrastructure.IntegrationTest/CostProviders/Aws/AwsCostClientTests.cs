//using Amazon;
//using Amazon.CostExplorer;
//using AutoMapper;
//using CostJanitor.Infrastructure.CostProviders.Aws;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace CostJanitor.Infrastructure.IntegrationTest.CostProviders.Aws
//{
//    // This assumes a valid AWS session with the right permissions (CostExplorer access)
//    public class AwsCostClientTests
//    {
//        private readonly IServiceProvider _services;

//        public AwsCostClientTests()
//        {
//            var coll = new ServiceCollection();

//            //TODO: Use a config builder to get a config object.
//            coll.AddInfrastructure(null);

//            _services = coll.BuildServiceProvider();
//        }

//        [Fact]
//        public async Task GetMonthlyTotalCostAllAccountsTest()
//        {
//            IAwsCostClient sut = new AwsCostClient(new AmazonCostExplorerClient(RegionEndpoint.USEast1), _services.GetService<IMapper>());
//            var resp = await sut.GetMonthlyTotalCostAllAccountsAsync();
            
//            Assert.NotNull(resp);

//            foreach (var result in resp)
//            {
//                Assert.NotEmpty(result.ResultsByTime);
//                Assert.NotEmpty(result.DimensionValueAttributes);
//            }
//        }

//        [Fact]
//        public async Task GetMonthlyTotalCostsByAccountId()
//        {
//            IAwsCostClient sut = new AwsCostClient(new AmazonCostExplorerClient(RegionEndpoint.USEast1), _services.GetService<IMapper>());
//            var resp = await sut.GetMonthlyTotalCostByAccountIdAsync("642375522597");
            
//            Assert.NotEmpty(resp.ResultsByTime);
//            Assert.NotEmpty(resp.DimensionValueAttributes);
//        }
//    }
//}