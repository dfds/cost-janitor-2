using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CostJanitor.Domain.Events.Report;
using Xunit;

namespace CostJanitor.Domain.UnitTest.Aggregates.Report
{
    public class ReportItemTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            Domain.Aggregates.ReportItem sut;

            sut = new Domain.Aggregates.ReportItem(Guid.Empty);
            
            Assert.NotNull(sut);
            Assert.True(sut.DomainEvents.Count == 1);
            Assert.Contains(sut.DomainEvents, i => i is ReportItemCreatedEvent);
        }
        
        [Fact]
        public void CanDetectValidConstruction()
        {
            //Arrange
            var sut = new Domain.Aggregates.ReportItem(Guid.NewGuid());
            var validationCtx = new ValidationContext(this);

            //Act
            var validationResults = sut.Validate(validationCtx);
            

            //Assert
            Assert.True(!validationResults.Any());
            Assert.Contains(sut.DomainEvents, i => i is ReportItemCreatedEvent);
        }
    }
}