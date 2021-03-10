using CostJanitor.Application.Commands.Report;
using CostJanitor.Application.IntegrationTest.Fixtures;
using CostJanitor.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.IntegrationTest
{
    public class ApplicationFacadeTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly ServiceProviderFixture _serviceProviderFixture;

        public ApplicationFacadeTests(ServiceProviderFixture serviceProviderFixture) 
        {
            _serviceProviderFixture = serviceProviderFixture;
        }

        [Fact]
        public async Task CanProcessCommand() 
        {
            //Arrange
            var sut = _serviceProviderFixture.Provider.GetService<IApplicationFacade>();
            var testCommand = new CreateReportCommand(new List<CostItem>());

            //Act
            var result = await sut.Execute(testCommand);

            //Assert
            Assert.NotNull(result);
        }
    }
}