using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CostJanitor.Infrastructure.EntityFramework
{
    public sealed class DomainDesignFactory : IDesignTimeDbContextFactory<DomainContext>
    {
        public DomainContext CreateDbContext(string[] args)
        {
            var connStr = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres";
            var connection = new Npgsql.NpgsqlConnection(connStr);

            connection.Open();
            
            var optionsBuilder = new DbContextOptionsBuilder<DomainContext>()
                .UseNpgsql(connection);

            return new DomainContext(optionsBuilder.Options, new FakeMediator());
        }
    }
}