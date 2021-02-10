using CostJanitor.Domain.Services;
using ResourceProvisioning.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands
{
    public sealed class DeleteCostItemCommandHandler : ICommandHandler<DeleteCostItemCommand, bool>
    {
        private readonly ICostService _costService;

        public DeleteCostItemCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<bool> Handle(DeleteCostItemCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.DeleteCostItemAsync(command.ReportItemId, command.Label, command.CapabilityIdentifier, cancellationToken);

            return report;
        }
    }
}