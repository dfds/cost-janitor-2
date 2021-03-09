using CostJanitor.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace CostJanitor.Application.UnitTest.Data
{
    public class ApplicationContextFixture : IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly NpgsqlConnection _connection;

        public ApplicationContextFixture()
        {
            _connection = new NpgsqlConnection("User ID=postgres;Password=local;Host=localhost;Port=5432;Database=postgres");

            _connection.Open();

            _options = new DbContextOptionsBuilder().UseNpgsql(_connection).Options;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public ApplicationContext GetDbContext(IMediator mediator = default)
        {
            return new ApplicationContext(_options, mediator);
        }
    }
}