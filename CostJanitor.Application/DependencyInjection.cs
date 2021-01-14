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

namespace CostJanitor.Application
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services, Action<ApplicationFacadeOptions> configureOptions = default)
		{
			var options = new ApplicationFacadeOptions();

			configureOptions?.Invoke(options);

			services.AddLogging();
			services.AddOptions<ApplicationFacadeOptions>()
					.Configure(configureOptions);
			services.AddCache();
			services.AddEntityFramework(options);
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
			
		}

		private static void AddEventHandlers(this IServiceCollection services)
		{

		}

		private static void AddRepositories(this IServiceCollection services)
		{
			
		}

		private static void AddServices(this IServiceCollection services)
		{
			
		}

		private static void AddFacade(this IServiceCollection services)
		{
//			services.AddTransient<IFacade, ApplicationFacade>();
		}

		private static void AddClients(this IServiceCollection services)
		{
		}

		private static void AddEntityFramework(this IServiceCollection services, ApplicationFacadeOptions brokerOptions = default)
		{
			services.AddDbContext<DomainContext>(options =>
			{
				var callingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
				var connectionString = brokerOptions?.ConnectionStrings?.GetValue<string>(nameof(DomainContext));

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

				using var context = new DomainContext(dbOptions, new FakeMediator());

				if (!context.Database.EnsureCreated())
				{
					return;
				}

				if (brokerOptions.EnableAutoMigrations)
				{
					context.Database.Migrate();
				}
			});

			services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<DomainContext>());
		}
	}
}
