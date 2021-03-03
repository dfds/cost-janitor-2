using CloudEngineering.CodeOps.Abstractions.Events;
using CostJanitor.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CostJanitor.Application.UnitTest.Data
{
    public class ApplicationContextTests : IClassFixture<ApplicationContextFixture>
    {
        private readonly ApplicationContextFixture _fixture;
        
        public ApplicationContextTests(ApplicationContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CanBeConstructed()
        {
            // Arrange
            var sut = _fixture.GetDbContext();

            // Act
            var hashCode = sut.GetType().GetHashCode();
            
            // Assert
            Assert.NotNull(sut);
            Assert.Equal(hashCode, sut.GetType().GetHashCode());
        }
        
        [Fact]
        public async Task CanPublishDomainEventsOnSaveEntities()
        {
            //Arrange
            var entityToAdd = new ReportRoot();
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()));
                        
            var sut = _fixture.GetDbContext(mockMediator.Object);

            //Act
            await sut.Database.MigrateAsync();

            var attachedEntity = await sut.AddAsync(entityToAdd);
            bool result = await sut.SaveEntitiesAsync(new CancellationToken());

            //Assert
            Assert.NotNull(sut);
            Assert.True(result);
            Assert.True(attachedEntity.Entity.Id != Guid.Empty);

            Mock.VerifyAll();
        }
    }
}