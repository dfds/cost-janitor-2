using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
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
            var report = await _costService.CreateOrAddCostItem(command.CostItemId, command.Label, command.Value, cancellationToken);

            return report;
        }
    }
}