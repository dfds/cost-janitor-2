using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using MediatR;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class GetReportByCapabilityIdentifierCommandHandler : ICommandHandler<GetReportByCapabilityIdentifierCommand, IEnumerable<ReportItem>>
    {
        private readonly ICostService _costService;

        public GetReportByCapabilityIdentifierCommandHandler(ICostService costService)
        {
            _costService = costService ?? throw new ArgumentNullException(nameof(costService));
        }

        public async Task<IEnumerable<ReportItem>> Handle(GetReportByCapabilityIdentifierCommand command, CancellationToken cancellationToken = default)
        {
            var report = await _costService.GetReportByCapabilityIdentifier(command.Identifier, cancellationToken);

            return report;
        }
    }
}