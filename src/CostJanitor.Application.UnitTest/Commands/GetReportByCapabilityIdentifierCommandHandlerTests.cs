using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Commands;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class GetReportByCapabilityIdentifierCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockCostService = new Mock<ICostService>();
            var sut = new GetReportByCapabilityIdentifierCommandHandler(mockCostService.Object);

            //Act
            var result = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(result, sut.GetHashCode());

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanHandleCommand()
        {
            //Arrange
            var mockCostService = new Mock<ICostService>();
            var sut = new GetReportByCapabilityIdentifierCommandHandler(mockCostService.Object);

            mockCostService
                .Setup(m => m.GetReportByCapabilityIdentifier(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Enumerable.Empty<ReportItem>()));

            //Act
            var result = await sut.Handle(new GetReportByCapabilityIdentifierCommand("a"));

            //Assert
            Assert.True(result != null);
            Assert.Empty(result);

            Mock.VerifyAll();
        }
    }
}