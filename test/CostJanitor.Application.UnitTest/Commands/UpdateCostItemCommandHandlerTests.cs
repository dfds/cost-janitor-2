using CostJanitor.Application.Commands;
using CostJanitor.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class UpdateCostItemCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange            
            var mockCostService = new Mock<ICostService>();
            var sut = new UpdateCostItemCommandHandler(mockCostService.Object);

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