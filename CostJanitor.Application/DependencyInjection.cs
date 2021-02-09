using AutoMapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Data;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Abstractions.Facade;
using ResourceProvisioning.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using CostJanitor.Application.Commands;
using CostJanitor.Application.Repositories;
using CostJanitor.Application.Services;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using CostJanitor.Infrastructure.EntityFramework;
using Microsoft.Extensions.Options;

namespace CostJanitor.Application
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddLogging();
			services.AddCache();
			services.AddEntityFramework(configuration);
//			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediator();
			services.AddBehaviors();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddRepositories();
			services.AddServices();
			services.AddClients();
			services.AddFacade();
		}

		private static void AddMediator(this IServiceCollection services)
		{
			services.AddTransient<ServiceFactory>(p => p.GetService);
			services.AddTransient<IMediator>(p => new Mediator(p.GetService<ServiceFactory>()));
		}

		private static void AddCache(this IServiceCollection services)
		{
//			services.AddSingleton<IMemoryCache, ApplicationCache>();
		}

		private static void AddBehaviors(this IServiceCollection services)
		{
//			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
//			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
		}

		private static void AddCommandHandlers(this IServiceCollection services)
		{
			// IRequestHandler
			services.AddTransient<IRequestHandler<CreateCostItemCommand, CostItem>, CreateCostItemCommandHandler>();
			services.AddTransient<IRequestHandler<CreateReportCommand, ReportItem>, CreateReportCommandHandler>();
			services.AddTransient<IRequestHandler<DeleteCostItemCommand, bool>, DeleteCostItemCommandHandler>();
			services.AddTransient<IRequestHandler<DeleteReportCommand, bool>, DeleteReportCommandHandler>();
			services.AddTransient<IRequestHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportItem>>, GetReportByCapabilityIdentifierCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateCostItemCommand, CostItem>, UpdateCostItemCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateReportCommand, ReportItem>, UpdateReportCommandHandler>();

			// ICommandHandler
			services.AddTransient<ICommandHandler<CreateCostItemCommand, CostItem>, CreateCostItemCommandHandler>();
			services.AddTransient<ICommandHandler<CreateReportCommand, ReportItem>, CreateReportCommandHandler>();
			services.AddTransient<ICommandHandler<DeleteCostItemCommand, bool>, DeleteCostItemCommandHandler>();
			services.AddTransient<ICommandHandler<DeleteReportCommand, bool>, DeleteReportCommandHandler>();
			services.AddTransient<ICommandHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportItem>>, GetReportByCapabilityIdentifierCommandHandler>();
			services.AddTransient<ICommandHandler<UpdateCostItemCommand, CostItem>, UpdateCostItemCommandHandler>();
			services.AddTransient<ICommandHandler<UpdateReportCommand, ReportItem>, UpdateReportCommandHandler>();
		}
		
		

		private static void AddEventHandlers(this IServiceCollection services)
		{

		}

		private static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IRepository<ReportItem>, ReportItemRepository>();
			services.AddTransient<IReportItemRepository, ReportItemRepository>();
		}
	
		private static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<ICostService, CostService>();
		}

		private static void AddFacade(this IServiceCollection services)
		{
			services.AddTransient<IFacade, ApplicationFacade>();
		}

		private static void AddClients(this IServiceCollection services)
		{
		}

		private static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<DomainContextOptions>(configuration);

			services.AddDbContext<DomainContext>(options =>
			{
				var serviceProvider = services.BuildServiceProvider();
				var dbContextOptions = serviceProvider.GetService<IOptions<DomainContextOptions>>();
				var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
				var connectionString = dbContextOptions.Value.ConnectionStrings?.GetValue<string>(nameof(DomainContext));

				if (string.IsNullOrEmpty(connectionString))
				{
					return;
				}

				services.AddSingleton(factory =>
				{
					var connection = new SqliteConnection(connectionString);

					connection.Open();

					return connection;
				});

				var dbOptions = options.UseSqlite(services.BuildServiceProvider().GetService<SqliteConnection>(),
					sqliteOptions =>
					{
						sqliteOptions.MigrationsAssembly(callingAssemblyName);
						sqliteOptions.MigrationsHistoryTable(callingAssemblyName + "_MigrationHistory");

					}).Options;

				using var context = new DomainContext(dbOptions, serviceProvider.GetService<IMediator>());

				if (context.Database.EnsureCreated())
				{
					return;
				}

				if (dbContextOptions.Value.EnableAutoMigrations)
				{
					context.Database.Migrate();
				}
			});

			services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<DomainContext>());
		}
		}
}
