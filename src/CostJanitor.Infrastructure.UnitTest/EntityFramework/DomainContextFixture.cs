using CostJanitor.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace CostJanitor.Infrastructure.UnitTest.EntityFramework
{
    public class DomainContextFixture : IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly NpgsqlConnection _connection;

        public DomainContextFixture()
        {
            _connection = new NpgsqlConnection("User ID=postgres;Password=local;Host=localhost;Port=5432;Database=postgres");

            _connection.Open();

            _options = new DbContextOptionsBuilder().UseNpgsql(_connection).Options;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public DomainContext GetDbContext(IMediator mediator = default)
        {
            return new DomainContext(_options, mediator);
        }
    }
}