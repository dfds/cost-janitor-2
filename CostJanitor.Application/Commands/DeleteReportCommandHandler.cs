using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand, bool>
    {
        private readonly ICostService _costService;

        public DeleteReportCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<bool> Handle(DeleteReportCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.DeleteReport(command.ReportId, cancellationToken);

            return report;
        }
    }
}