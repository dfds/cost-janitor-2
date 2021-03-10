using CostJanitor.Application.Commands.Report;
using System;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Report
{
    public class DeleteReportCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var id = Guid.NewGuid();
            var sut = new DeleteReportCommand(id);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal(id, sut.ReportId);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DeleteReportCommand(Guid.NewGuid());

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            DeleteReportCommand sut;
            var json = "{\"reportId\":\"6361867a-8518-4715-995e-433bf961f344\"}";

            //Act
            sut = JsonSerializer.Deserialize<DeleteReportCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(new Guid("6361867a-8518-4715-995e-433bf961f344"), sut.ReportId);
        }
    }
}