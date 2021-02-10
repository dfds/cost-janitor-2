using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ResourceProvisioning.Abstractions.Events;
using Xunit;

namespace CostJanitor.Infrastructure.UnitTest.EntityFramework
{
    public class DomainContextTests : IClassFixture<DomainContextFixture>
    {
        private readonly DomainContextFixture _fixture;
        
        public DomainContextTests(DomainContextFixture fixture)
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
            var entityToAdd = new ReportItem(Guid.NewGuid());
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()));
                        
            var sut = _fixture.GetDbContext(mockMediator.Object);

            //Act
            await sut.Database.MigrateAsync();

            var attachedEntity = await sut.AddAsync(entityToAdd);
            var result = await sut.SaveEntitiesAsync(new CancellationToken());

            //Assert
            Assert.NotNull(sut);
            Assert.True(result);
            Assert.True(attachedEntity.Entity.Id != Guid.Empty);

            Mock.VerifyAll();
        }
    }
}