using CostJanitor.Application.Commands.Report;
using System.Text.Json;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands.Report
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
            Assert.True(sut.CapabilityIdentifier != string.Empty);
            Assert.Equal("a", sut.CapabilityIdentifier);
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
            var json = "{\"capabilityIdentifier\":\"a\"}";

            //Act
            sut = JsonSerializer.Deserialize<GetReportByCapabilityIdentifierCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal("a", sut.CapabilityIdentifier);
        }
    }
}