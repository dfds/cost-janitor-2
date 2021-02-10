using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Commands;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using Xunit;
using CostJanitor.Domain.ValueObjects;

namespace CostJanitor.Application.UnitTest.Commands
{
    public class UpdateReportCommandHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange            
            var mockCostService = new Mock<ICostService>();
            var sut = new UpdateReportCommandHandler(mockCostService.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.Equal(hashCode, sut.GetHashCode());
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public Task CanHandleCommand()
        {
            throw new NotImplementedException();
        }
    }
}