using System;
using System.Text.Json;
using CostJanitor.Application.Commands;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class DeleteCostItemCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var id = new Guid();
            var sut = new DeleteCostItemCommand(id);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.Equal(id, sut.CostItemId);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new DeleteCostItemCommand(new Guid());

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
            var json = "{\"costItemId\":\"6361867a-8518-4715-995e-433bf961f344\"}";

            //Act
            sut = JsonSerializer.Deserialize<DeleteCostItemCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(new Guid("6361867a-8518-4715-995e-433bf961f344"), sut.CostItemId);
        }
    }
}