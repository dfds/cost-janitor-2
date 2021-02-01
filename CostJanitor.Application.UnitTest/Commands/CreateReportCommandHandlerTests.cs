using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Commands;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class CreateReportCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange            
            var mockCostService = new Mock<ICostService>();
            var sut = new CreateReportCommandHandler(mockCostService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanHandleCommand()
        {
            //Arrange
            var mockCostService = new Mock<ICostService>();
            var reportItemId = Guid.NewGuid();
            var reportItem = new ReportItem(reportItemId);

            mockCostService.Setup(m => m.CreateOrAddReport(It.IsAny<Guid>(), It.IsAny<IEnumerable<CostItem>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(reportItem));

            var sut = new CreateReportCommandHandler(mockCostService.Object);

            //Act
            var result = await sut.Handle(new CreateReportCommand(reportItemId, new List<CostItem>()));

            //Assert
            Assert.Equal(result, reportItem);

            Mock.VerifyAll();
        }
    }
}