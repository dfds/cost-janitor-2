using System;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

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
            var report = await _costService.DeleteCostItem(command.CostItemId, cancellationToken);

            return report;
        }
    }
}