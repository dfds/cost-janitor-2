using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using CloudEngineering.CodeOps.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Commands
{
    public sealed class GetReportByCapabilityIdentifierCommandHandler : ICommandHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportRoot>>
    {
        private readonly ICostService _costService;

        public GetReportByCapabilityIdentifierCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<IEnumerable<ReportRoot>> Handle(GetReportByCapabilityIdentifierCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.GetReportByCapabilityIdentifierAsync(command.CapabilityIdentifier, cancellationToken);

            return report;
        }
    }
}