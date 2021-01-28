using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Application.Services;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using Moq;
using ResourceProvisioning.Abstractions.Data;
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
            var mockCostItemRepository = new Mock<ICostItemRepository>();
            var mockReportItemRepository = new Mock<IReportItemRepository>();

            //Act
            sut = new CostService(mockCostItemRepository.Object, mockReportItemRepository.Object);

            //Assert
            Assert.NotNull(sut);

            Mock.VerifyAll();
        }
        
        [Fact]
        public async Task CanAddCostItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCostItemRepository = new Mock<ICostItemRepository>();
            var mockReportItemRepository = new Mock<IReportItemRepository>();
            var costItem = new CostItem("a", "b", "c");

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockCostItemRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockCostItemRepository.Setup(m => m.Add(It.IsAny<CostItem>())).Returns(costItem);

            var sut = new CostService(mockCostItemRepository.Object, mockReportItemRepository.Object);

            //Act
            var result = await sut.CreateOrAddCostItem("c", "a", "b");

            //Assert
            Assert.Equal(result, costItem);

            Mock.VerifyAll();
        }
        
        [Fact]
        public async Task CanAddReportItem()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCostItemRepository = new Mock<ICostItemRepository>();
            var mockReportItemRepository = new Mock<IReportItemRepository>();
            var reportId = Guid.NewGuid();
            var reportItem = new ReportItem(reportId);

            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportItemRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportItemRepository.Setup(m => m.Add(It.IsAny<ReportItem>())).Returns(reportItem);

            var sut = new CostService(mockCostItemRepository.Object, mockReportItemRepository.Object);

            //Act
            var result = await sut.CreateOrAddReport(reportId, new List<CostItem>());

            //Assert
            Assert.Equal(result, reportItem);

            Mock.VerifyAll();
        }        
        
        [Fact]
        public async Task CanDeleteReportItem()
        {
            //Arrange
            var reportId = Guid.NewGuid();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCostItemRepository = new Mock<ICostItemRepository>();
            var mockReportItemRepository = new Mock<IReportItemRepository>();
            var reportItem = new ReportItem(reportId);


            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockReportItemRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockReportItemRepository.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(reportItem));
            mockReportItemRepository.Setup(m => m.Delete(It.IsAny<ReportItem>()));

            var sut = new CostService(mockCostItemRepository.Object, mockReportItemRepository.Object);

            //Act
            await sut.DeleteReport(reportId);

            //Assert
            Mock.VerifyAll();
        }

        [Fact]
        public async Task CanDeleteCostItem()
        {
            //Arrange
            var costId = Guid.NewGuid();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCostItemRepository = new Mock<ICostItemRepository>();
            var mockReportItemRepository = new Mock<IReportItemRepository>();
            var costItem = new CostItem("a", "b", "c");
            costItem.SetId(costId);


            mockUnitOfWork.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            mockCostItemRepository.SetupGet(m => m.UnitOfWork).Returns(mockUnitOfWork.Object);
            mockCostItemRepository.Setup(m => m.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(costItem));
            mockCostItemRepository.Setup(m => m.Delete(It.IsAny<CostItem>()));

            var sut = new CostService(mockCostItemRepository.Object, mockReportItemRepository.Object);

            //Act
            await sut.DeleteCostItem(costId);
            //Assert
            Mock.VerifyAll();
        }
    }
}