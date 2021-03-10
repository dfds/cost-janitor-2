using CostJanitor.Application.Commands.Cost;
using System;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Cost
{
    public class CreateCostItemCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new CreateCostItemCommand("a", "b", "c", Guid.NewGuid());
            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.True(!string.IsNullOrEmpty(sut.CapabilityIdentifier));
            Assert.True(!string.IsNullOrEmpty(sut.Label));
            Assert.True(!string.IsNullOrEmpty(sut.Value));
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new CreateCostItemCommand("a", "b", "c", Guid.NewGuid());


            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            CreateCostItemCommand sut;
            var json = "{\"capabilityIdentifier\": \"a\", \"label\": \"b\", \"value\": \"c\"}";

            //Act
            sut = JsonSerializer.Deserialize<CreateCostItemCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("a", sut.CapabilityIdentifier);
            Assert.Equal("b", sut.Label);
            Assert.Equal("c", sut.Value);
        }
    }
}
