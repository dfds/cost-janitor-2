using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Abstractions.Facade;
using CloudEngineering.CodeOps.Abstractions.Repositories;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using CostJanitor.Application.Commands.Cost;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Application.Data;
using CostJanitor.Application.Repositories;
using CostJanitor.Application.Services;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using CostJanitor.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CostJanitor.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //Framework dependencies
            services.AddLogging();

            //External dependencies
            services.AddInfrastructure(configuration);

            //Application dependencies
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediator();
            services.AddApplicationContext(configuration);
            services.AddCommandHandlers();
            services.AddFacade();
            services.AddRepositories();
            services.AddServices();
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddSingleton<ServiceFactory>(p => p.GetService);
            services.AddSingleton<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
        }

        private static void AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EntityContextOptions>(configuration);

            services.AddDbContext<ApplicationContext>(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var dbContextOptions = serviceProvider.GetService<IOptions<EntityContextOptions>>();
                var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var connectionString = dbContextOptions.Value.ConnectionStrings?.GetValue<string>(nameof(ApplicationContext));

                Console.WriteLine("Creating ApplicationContext with connStr: " + connectionString);

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ApplicationFacadeException($"Could not find connection string with entry key: {nameof(ApplicationContext)}");
                }

                services.AddSingleton(factory =>
                {
                    var connection = new NpgsqlConnection(connectionString);

                    Console.WriteLine("Opening connection with: " + connectionString);

                    connection.Open();

                    return connection;
                });

                var dbOptions = options.UseNpgsql(services.BuildServiceProvider().GetService<NpgsqlConnection>(),
                    sqliteOptions =>
                    {
                        sqliteOptions.MigrationsAssembly(callingAssemblyName);
                        sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");

                    }).Options;

                using var context = new ApplicationContext(dbOptions, serviceProvider.GetService<IMediator>());

                if (dbContextOptions.Value.EnableAutoMigrations)
                {
                    Console.WriteLine("Migrating db with connection: " + connectionString);

                    context.Database.Migrate();
                }
            });

            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ApplicationContext>());
        }

        private static void AddCommandHandlers(this IServiceCollection services)
        {
            // IRequestHandler
            services.AddTransient<IRequestHandler<CreateCostItemCommand, CostItem>, CreateCostItemCommandHandler>();
            services.AddTransient<IRequestHandler<CreateReportCommand, ReportRoot>, CreateReportCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteCostItemCommand, bool>, DeleteCostItemCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteReportCommand, bool>, DeleteReportCommandHandler>();
            services.AddTransient<IRequestHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportRoot>>, GetReportByCapabilityIdentifierCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateCostItemCommand, CostItem>, UpdateCostItemCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateReportCommand, ReportRoot>, UpdateReportCommandHandler>();

            // ICommandHandler
            services.AddTransient<ICommandHandler<CreateCostItemCommand, CostItem>, CreateCostItemCommandHandler>();
            services.AddTransient<ICommandHandler<CreateReportCommand, ReportRoot>, CreateReportCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteCostItemCommand, bool>, DeleteCostItemCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteReportCommand, bool>, DeleteReportCommandHandler>();
            services.AddTransient<ICommandHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportRoot>>, GetReportByCapabilityIdentifierCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateCostItemCommand, CostItem>, UpdateCostItemCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateReportCommand, ReportRoot>, UpdateReportCommandHandler>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<ReportRoot>, ReportRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICostService, CostService>();
        }

        private static void AddFacade(this IServiceCollection services)
        {
            services.AddTransient<IFacade, ApplicationFacade>();
        }
    }
}
