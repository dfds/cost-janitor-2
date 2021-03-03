//using System;
//using System.Collections.Generic;
//using System.Text.Json;
//using CostJanitor.Application.Commands;
//using CostJanitor.Domain.Aggregates;
//using CostJanitor.Domain.ValueObjects;
//using Xunit;

//namespace CostJanitor.Application.UnitTest.Commands
//{
//    public class UpdateReportCommandTests
//    {
//        [Fact]
//        public void CanBeConstructed()
//        {
//            //Arrange
//            var sut = new UpdateReportCommand(Guid.NewGuid(), new List<CostItem>());
//            //Act
//            var hashCode = sut.GetHashCode();

//            //Assert
//            Assert.NotNull(sut);
//            Assert.Equal(hashCode, sut.GetHashCode());
//            Assert.True(sut.ReportId != Guid.Empty);
//            Assert.Empty(sut.CostItems);
//        }

//        [Fact]
//        public void CanBeSerialized()
//        {
//            //Arrange
//            var sut = new UpdateReportCommand(Guid.NewGuid(), new List<CostItem>());

//            //Act
//            var json = JsonSerializer.Serialize(sut);

//            //Assert
//            Assert.False(string.IsNullOrEmpty(json));
//        }

//        [Fact]
//        public void CanBeDeserialized()
//        {
//            //Arrange
//            UpdateReportCommand sut;
//            var json = "{\"reportId\": \"5c19ccdd-3e15-4874-a391-6087f4fdfa5d\", \"costItems\": []}";

//            //Act
//            sut = JsonSerializer.Deserialize<UpdateReportCommand>(json);

//            //Assert
//            Assert.NotNull(sut);
//            Assert.Equal(Guid.Parse("5c19ccdd-3e15-4874-a391-6087f4fdfa5d"), sut.ReportId);
//            Assert.Empty(sut.CostItems);
//        }
//    }
//}