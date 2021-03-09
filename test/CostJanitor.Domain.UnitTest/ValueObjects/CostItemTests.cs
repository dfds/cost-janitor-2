using CostJanitor.Domain.ValueObjects;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Domain.UnitTest.ValueObjects
{
    public class CostItemTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            CostItem sut;

            //Act
            sut = new CostItem("label", "value", "identifier");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("identifier", sut.CapabilityIdentifier);
            Assert.Equal("label", sut.Label);
            Assert.Equal("value", sut.Value);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new CostItem("label", "value", "identifier");

            //Act
            var payload = JsonSerializer.Serialize(sut, new JsonSerializerOptions { IgnoreNullValues = true });

            //Assert
            Assert.NotNull(JsonDocument.Parse(payload));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            CostItem sut;

            //Act
            sut = JsonSerializer.Deserialize<CostItem>("{\"capabilityIdentifier\":\"identifier\",\"value\":\"value\",\"label\":\"label\"}");

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("identifier", sut.CapabilityIdentifier);
            Assert.Equal("label", sut.Label);
            Assert.Equal("value", sut.Value);
        }
    }
}