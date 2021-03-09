using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Events.Report;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace CostJanitor.Domain.UnitTest.Aggregates.Report
{
    public class ReportRootTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            var sut = new ReportRoot();

            Assert.NotNull(sut);
            Assert.True(sut.DomainEvents.Count == 1);
            Assert.Contains(sut.DomainEvents, i => i is ReportCreatedEvent);
        }

        [Fact]
        public void CanDetectValidConstruction()
        {
            //Arrange
            var sut = new ReportRoot();
            var validationCtx = new ValidationContext(this);

            //Act
            var validationResults = sut.Validate(validationCtx);

            //Assert
            Assert.True(!validationResults.Any());
            Assert.Contains(sut.DomainEvents, i => i is ReportCreatedEvent);
        }
    }
}