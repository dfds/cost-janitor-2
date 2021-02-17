using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands
{
    public sealed class UpdateReportCommandHandler : ICommandHandler<UpdateReportCommand, ReportRoot>
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
    }
}