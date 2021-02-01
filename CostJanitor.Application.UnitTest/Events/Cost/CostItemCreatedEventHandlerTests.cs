using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using CostJanitor.Application.Events.Cost;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Cost;
using Xunit;

namespace CostJanitor.Application.UnitTest.Events.Cost
{
    public class CostItemCreatedEventHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var sut = new CostItemCreatedEventHandler(mockMapper.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanHandleEvent()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var sut = new CostItemCreatedEventHandler(mockMapper.Object);

            //Act
            await sut.Handle(new CostItemCreatedEvent(new CostItem("a", "b", "c")));

            //Assert
            Mock.VerifyAll();
        }
    }
}
