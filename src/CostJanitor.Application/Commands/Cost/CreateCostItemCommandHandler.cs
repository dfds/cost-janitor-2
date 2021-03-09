using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands.Cost
{
    public sealed class CreateCostItemCommandHandler : ICommandHandler<CreateCostItemCommand, CostItem>
    {
        private readonly ICostService _costService;

        public CreateCostItemCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<CostItem> Handle(CreateCostItemCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.AddOrUpdateCostItemAsync(command.ReportItemId, command.CapabilityIdentifier, command.Label, command.Value, cancellationToken);

            return report;
        }
    }
}