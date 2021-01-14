using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class UpdateReportCommandHandler : ICommandHandler<UpdateReportCommand, ReportItem>
    {
        private readonly ICostService _costService;

        public UpdateReportCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<ReportItem> Handle(UpdateReportCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.CreateOrAddReport(command.ReportId, command.CostItems, cancellationToken);

            return report;
        }
    }
}