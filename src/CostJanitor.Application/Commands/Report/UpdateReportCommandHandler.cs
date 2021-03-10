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
    public sealed class UpdateReportCommandHandler : ICommandHandler<UpdateReportCommand, ReportRoot>, ICommandHandler<UpdateReportCommand, IAggregateRoot>
    {
        private readonly ICostService _costService;

        public UpdateReportCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<ReportRoot> Handle(UpdateReportCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.UpdateReportAsync(command.Report, cancellationToken);

            return report;
        }

        async Task<IAggregateRoot> IRequestHandler<UpdateReportCommand, IAggregateRoot>.Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }

        async Task<IAggregateRoot> ICommandHandler<UpdateReportCommand, IAggregateRoot>.Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request, cancellationToken);
        }
    }
}