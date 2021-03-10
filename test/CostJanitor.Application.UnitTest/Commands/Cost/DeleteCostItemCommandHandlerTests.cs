using CostJanitor.Application.Commands.Cost;
using CostJanitor.Domain.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Cost
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
            var mockCostService = new Mock<ICostService>();
            var sut = new DeleteCostItemCommandHandler(mockCostService.Object);

            mockCostService.Setup(m => m.DeleteCostItemAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var result = await sut.Handle(new DeleteCostItemCommand(Guid.NewGuid(), "label", "identifier"));

            //Assert
            Assert.True(result);

            Mock.VerifyAll();
        }
    }
}