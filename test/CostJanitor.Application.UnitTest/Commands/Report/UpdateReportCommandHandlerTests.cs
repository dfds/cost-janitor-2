using CostJanitor.Application.Commands;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Report
{
    public class UpdateReportCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange            
            var mockCostService = new Mock<ICostService>();
            var sut = new UpdateReportCommandHandler(mockCostService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public Task CanHandleCommand()
        {
            throw new NotImplementedException();
        }
    }
}