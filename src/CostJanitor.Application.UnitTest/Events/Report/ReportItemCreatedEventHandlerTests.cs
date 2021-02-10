//using AutoMapper;
//using Moq;
//using System;
//using System.Threading.Tasks;
//using CostJanitor.Application.Events.Cost;
//using CostJanitor.Application.Events.Report;
//using CostJanitor.Domain.Aggregates;
//using CostJanitor.Domain.Events.Cost;
//using CostJanitor.Domain.Events.Report;
//using MediatR;
//using Xunit;

//namespace CostJanitor.Application.UnitTest.Events.Report
//{
//    public class ReportItemCreatedEventHandlerTests
//    {
//        [Fact]
//        public void CanBeConstructed()
//        {
//            //Arrange
//            var mockMapper = new Mock<IMapper>();
//            var mockMediator = new Mock<IMediator>();
//            var sut = new ReportItemCreatedEventHandler(mockMapper.Object, mockMediator.Object);

//            //Act
//            var hashCode = sut.GetHashCode();

//            //Assert
//            Assert.NotNull(sut);
//            Assert.Equal(hashCode, sut.GetHashCode());

//            Mock.VerifyAll();
//        }

//        [Fact]
//        public async Task CanHandleEvent()
//        {
//            //Arrange
//            var mockMapper = new Mock<IMapper>();
//            var mockMediator = new Mock<IMediator>();
//            var sut = new ReportItemCreatedEventHandler(mockMapper.Object, mockMediator.Object);

//            //Act
//            await sut.Handle(new ReportItemCreatedEvent(new ReportItem(Guid.NewGuid())));

//            //Assert
//            Mock.VerifyAll();
//        }
//    }
//}