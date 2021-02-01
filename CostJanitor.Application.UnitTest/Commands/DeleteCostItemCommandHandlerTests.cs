using System;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Commands;
using CostJanitor.Domain.Services;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class DeleteCostItemCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockCostService = new Mock<ICostService>();
            var sut = new DeleteCostItemCommandHandler(mockCostService.Object);

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
            var costItemId = Guid.NewGuid();
            var mockCostService = new Mock<ICostService>();
            var sut = new DeleteCostItemCommandHandler(mockCostService.Object);

            mockCostService.Setup(m => m.DeleteCostItem(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var result = await sut.Handle(new DeleteCostItemCommand(costItemId));

            //Assert
            Assert.True(result);

            Mock.VerifyAll();
        }
    }
}