using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class CreateReportCommandHandler : ICommandHandler<CreateReportCommand, ReportItem>
    {
        private readonly ICostService _costService;

        public CreateReportCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<ReportItem> Handle(CreateReportCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.CreateOrAddReport(command.ReportId, command.CostItems, cancellationToken);

            return report;
        }
    }
}