using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

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
            var report = await _costService.CreateOrAddCostItem(command.CapabilityIdentifier, command.Label, command.Value, cancellationToken);

            return report;
        }
    }
}