using CostJanitor.Infrastructure.CostProviders.Aws.Mapping.Profiles;
using Xunit;

namespace CostJanitor.Infrastructure.UnitTest.Aws.Profiles
{
    public class DefaultProfileTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            //Arrange
            DefaultProfile sut;

            //Act
            sut = new DefaultProfile();

            //Assert
            Assert.NotNull(sut);
        }
    }
}
