using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CostJanitor.Domain.Events.Cost;
using Xunit;

namespace CostJanitor.Domain.UnitTest.Aggregates.Cost
{
    public class CostItemTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            CostJanitor.Domain.Aggregates.CostItem sut;

            sut = new Domain.Aggregates.CostItem("", "", "");
            
            Assert.NotNull(sut);
            Assert.True(sut.DomainEvents.Count == 1);
            Assert.Contains(sut.DomainEvents, i => i is CostItemCreatedEvent);
        }
        
        [Fact]
        public void CanDetectValidConstruction()
        {
            //Arrange
            var sut = new Domain.Aggregates.CostItem("a", "b", "c");
            var validationCtx = new ValidationContext(this);

            //Act
            sut.SetId(Guid.NewGuid());
            var validationResults = sut.Validate(validationCtx);
            

            //Assert
            Assert.True(!validationResults.Any());
            Assert.Contains(sut.DomainEvents, i => i is CostItemIdChangedEvent);
        }
        
        [Fact]
        public void CanDetectInvalidConstruction()
        {
            //Arrange
            var sut = new Domain.Aggregates.CostItem("a", "b", "c");
            var validationCtx = new ValidationContext(this);

            //Act
            var validationResults = sut.Validate(validationCtx);

            //Assert
            Assert.True(validationResults.Count() == 1);
        }
    }
}