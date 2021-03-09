using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Aggregates;
using System;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Report
{
    public class UpdateReportCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var fakeReport = new ReportRoot();
            var sut = new UpdateReportCommand(fakeReport);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal(fakeReport, sut.Report);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var fakeReport = new ReportRoot();
            var sut = new UpdateReportCommand(fakeReport);

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            UpdateReportCommand sut;
            var json = "{\"report\":{\"CostItems\":[],\"DomainEvents\":[{}],\"Id\":\"00000000-0000-0000-0000-000000000000\"}}";

            //Act
            sut = JsonSerializer.Deserialize<UpdateReportCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(Guid.Empty, sut.Report.Id);
        }
    }
}