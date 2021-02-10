using CostJanitor.Application.Commands;
using CostJanitor.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class CreateReportCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new CreateReportCommand(new List<CostItem>());

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Empty(sut.CostItems);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new CreateReportCommand(new List<CostItem>());

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            CreateReportCommand sut;
            var json = "{\"costItems\": []}";

            //Act
            sut = JsonSerializer.Deserialize<CreateReportCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Empty(sut.CostItems);
        }
    }
}