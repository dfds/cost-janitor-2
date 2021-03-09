using CostJanitor.Application.Commands;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Report
{
    public class DeleteReportCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockCostService = new Mock<ICostService>();
            var sut = new DeleteReportCommandHandler(mockCostService.Object);

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
            var reportId = Guid.NewGuid();
            var mockCostService = new Mock<ICostService>();
            var sut = new DeleteReportCommandHandler(mockCostService.Object);

            mockCostService.Setup(m => m.DeleteReportAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var result = await sut.Handle(new DeleteReportCommand(reportId));

            //Assert
            Assert.True(result);

            Mock.VerifyAll();
        }
    }
}