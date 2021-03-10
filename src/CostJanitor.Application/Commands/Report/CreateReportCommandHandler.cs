using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands.Report
{
    public sealed class CreateReportCommandHandler : ICommandHandler<CreateReportCommand, ReportRoot>, IRequestHandler<CreateReportCommand, IAggregateRoot>
    {
        private readonly ICostService _costService;

        public CreateReportCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<ReportRoot> Handle(CreateReportCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.AddReportAsync(command.CostItems, cancellationToken);

            return report;
        }

        async Task<IAggregateRoot> IRequestHandler<CreateReportCommand, IAggregateRoot>.Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}