using CostJanitor.Application.Commands;
using CostJanitor.Application.Repositories;
using CostJanitor.Application.Services;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using CostJanitor.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceProvisioning.Abstractions.Commands;
using ResourceProvisioning.Abstractions.Facade;
using ResourceProvisioning.Abstractions.Repositories;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddInfrastructure(configuration);
			
            //Application dependencies
			services.AddBehaviors();
			services.AddCommandHandlers();
			services.AddEventHandlers();
			services.AddRepositories();
			services.AddServices();
			services.AddFacade();
		}

		private static void AddBehaviors(this IServiceCollection services)
		{
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
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
		
		private static void AddEventHandlers(this IServiceCollection services)
		{

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
