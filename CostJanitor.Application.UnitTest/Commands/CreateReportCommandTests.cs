using System;
using System.Collections.Generic;
using System.Text.Json;
using CostJanitor.Application.Commands;
using CostJanitor.Domain.Aggregates;
using Xunit;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class CreateReportCommandTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var sut = new CreateReportCommand(Guid.NewGuid(), new List<CostItem>());
            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.True(sut.ReportId != Guid.Empty);
            Assert.Empty(sut.CostItems);
        }

        [Fact]
        public void CanBeSerialized()
        {
            //Arrange
            var sut = new CreateReportCommand(Guid.NewGuid(), new List<CostItem>());

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
            var json = "{\"reportId\": \"c152244c-6132-4503-be43-17a18afd6af1\", \"costItems\": []}";

            //Act
            sut = JsonSerializer.Deserialize<CreateReportCommand>(json);

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(Guid.Parse("c152244c-6132-4503-be43-17a18afd6af1"), sut.ReportId);
            Assert.Empty(sut.CostItems);
        }
    }
}