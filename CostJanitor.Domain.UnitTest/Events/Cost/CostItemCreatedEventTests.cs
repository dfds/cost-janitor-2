using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Cost;
using Xunit;

namespace CostJanitor.Domain.UnitTest.Events.Cost
{
    public class CostItemCreatedEventTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            CostItemCreatedEvent sut;

            //Act
            sut = new CostItemCreatedEvent(null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.CostItem == null);
        }

        [Fact]
        public void AreNotEqual()
        {
            //Arrange
            var costItemRoot = new CostItem(string.Empty, string.Empty, string.Empty);
            var sut = new CostItemCreatedEvent(costItemRoot);

            //Act
            var anotherEvent = new CostItemCreatedEvent(costItemRoot);

            //Assert
            Assert.True(sut.CostItem == costItemRoot);
            Assert.True(anotherEvent.CostItem == costItemRoot);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}