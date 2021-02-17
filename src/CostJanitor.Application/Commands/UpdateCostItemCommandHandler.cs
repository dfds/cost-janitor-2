using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands
{
    public sealed class UpdateCostItemCommandHandler : ICommandHandler<UpdateCostItemCommand, CostItem>
    {
        private readonly ICostService _costService;

        public UpdateCostItemCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<CostItem> Handle(UpdateCostItemCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.AddOrUpdateCostItemAsync(command.ReportItemId, command.CapabilityIdentifier, command.Label, command.Value, cancellationToken);

            return report;
        }
    }
}