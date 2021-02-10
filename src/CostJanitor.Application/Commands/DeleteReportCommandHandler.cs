using CostJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var report = await _costService.DeleteReportAsync(command.ReportId, cancellationToken);

            return report;
        }
    }
}