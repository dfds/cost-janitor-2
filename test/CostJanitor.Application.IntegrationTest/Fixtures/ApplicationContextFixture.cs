using CostJanitor.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace CostJanitor.Application.IntegrationTest.Fixtures
{
    public class ApplicationContextFixture : IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly NpgsqlConnection _connection;
        private bool _disposedValue;

        public ApplicationContextFixture()
        {
            _connection = new NpgsqlConnection("User ID=postgres;Password=local;Host=localhost;Port=5432;Database=postgres");

            _connection.Open();

            _options = new DbContextOptionsBuilder().UseNpgsql(_connection).Options;
        }

        public ApplicationContext GetDbContext(IMediator mediator = default)
        {
            return new ApplicationContext(_options, mediator);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}