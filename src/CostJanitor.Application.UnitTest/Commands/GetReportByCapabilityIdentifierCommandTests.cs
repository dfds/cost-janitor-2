using System;
using System.Text.Json;
using CostJanitor.Application.Commands;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class GetReportByCapabilityIdentifierCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new GetReportByCapabilityIdentifierCommand("a");

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.True(sut.Identifier != String.Empty);
            Assert.Equal("a", sut.Identifier);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new GetReportByCapabilityIdentifierCommand("a");

            //Act
            var json = JsonSerializer.Serialize(sut);

            //Assert
            Assert.False(string.IsNullOrEmpty(json));
        }

        [Fact]
        public void CanBeDeserialized()
        {
            //Arrange
            GetReportByCapabilityIdentifierCommand sut;
            var json = "{\"identifier\":\"a\"}";

            //Act
            sut = JsonSerializer.Deserialize<GetReportByCapabilityIdentifierCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("a", sut.Identifier);
        }
    }
}