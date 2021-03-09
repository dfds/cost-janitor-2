using CostJanitor.Application.Commands;
using CostJanitor.Application.Commands.Cost;
using System;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Cost
{
    public class DeleteCostItemCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var reportId = Guid.NewGuid();
            var sut = new DeleteCostItemCommand(reportId, "mylabel", "mycapabilityidentifier");

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal(reportId, sut.ReportItemId);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DeleteCostItemCommand(Guid.NewGuid(), "mylabel", "mycapabilityidentifier");

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            DeleteCostItemCommand sut;
            var json = "{\"reportItemId\":\"6361867a-8518-4715-995e-433bf961f344\"}";

            //Act
            sut = JsonSerializer.Deserialize<DeleteCostItemCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(new Guid("6361867a-8518-4715-995e-433bf961f344"), sut.ReportItemId);
        }
    }
}