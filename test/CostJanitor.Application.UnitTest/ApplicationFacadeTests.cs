using MediatR;
using Moq;
using Xunit;

namespace CostJanitor.Application.UnitTest
{
    public class ApplicationFacadeTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            var sut = new ApplicationFacade(mockMediator.Object);

            //Act
            var hashCode = sut.GetHashCode();

            //Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetHashCode());

            Mock.VerifyAll();
        }
    }
}