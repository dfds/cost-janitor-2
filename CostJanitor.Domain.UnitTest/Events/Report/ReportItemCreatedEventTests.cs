using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Report;
using System;
using Xunit;

namespace CostJanitor.Domain.UnitTest.Events.Cost
{
    public class ReportItemCreatedEventTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            ReportItemCreatedEvent sut;

            //Act
            sut = new ReportItemCreatedEvent(null);

            //Assert
            Assert.NotNull(sut);
            Assert.True(sut.ReportItem == null);
        }

        [Fact]
        public void AreNotEqual()
        {
            //Arrange
            var reportItem = new ReportItem(Guid.NewGuid());
            var sut = new ReportItemCreatedEvent(reportItem);

            //Act
            var anotherEvent = new ReportItemCreatedEvent(reportItem);

            //Assert
            Assert.True(sut.ReportItem == reportItem);
            Assert.True(anotherEvent.ReportItem == reportItem);
            Assert.False(sut.Equals(anotherEvent));
        }
    }
}