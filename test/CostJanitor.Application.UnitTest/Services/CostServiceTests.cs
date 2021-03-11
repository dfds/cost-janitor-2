using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Services;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.ValueObjects;
using Moq;
using CloudEngineering.CodeOps.Abstractions.Data;
using Xunit;

namespace CostJanitor.Application.UnitTest.Services
{
    public class CostServiceTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            CostService sut;
            var mockReportRepository = new Mock<IReportRepository>();

            //Act
            sut = new CostService(mockReportRepository.Object);

            //Assert
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanAddCostItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReportRepository = new Mock<IReportRepository>();
            var fakeReport = new ReportRoot();

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportRepository.Setup(m => m.Add(It.IsAny<ReportRoot>())).Returns(fakeReport);

            var sut = new CostService(mockReportRepository.Object);

            //Act
            var result = await sut.AddReportAsync(new[] { new CostItem("a", "b", "c") });

            //Assert
            Assert.NotNull(result);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanAddReportItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReportRepository = new Mock<IReportRepository>();
            var fakeReport = new ReportRoot();

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportRepository.Setup(m => m.Add(It.IsAny<ReportRoot>())).Returns(fakeReport);

            var sut = new CostService(mockReportRepository.Object);

            //Act
            var result = await sut.AddReportAsync(new List<CostItem>());

            //Assert
            Assert.Equal(result, fakeReport);

            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanDeleteReportItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReportRepository = new Mock<IReportRepository>();
            var fakeReport = new ReportRoot();

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportRepository.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(fakeReport));
            mockReportRepository.Setup(m => m.Delete(It.IsAny<ReportRoot>()));

            var sut = new CostService(mockReportRepository.Object);

            //Act
            await sut.DeleteReportAsync(Guid.NewGuid());

            //Assert
            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanDeleteCostItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReportRepository = new Mock<IReportRepository>();
            var fakeReport = new ReportRoot();

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportRepository.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(fakeReport));
            mockReportRepository.Setup(m => m.Delete(It.IsAny<ReportRoot>()));

            var sut = new CostService(mockReportRepository.Object);

            //Act
            await sut.DeleteCostItemAsync(Guid.NewGuid(), "a");

            //Assert
            Mock.VerifyAll();
        }
    }
}