using CloudEngineering.CodeOps.Abstractions.Commands;
using CostJanitor.Domain.Aggregates;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CostJanitor.Application.Commands.Report
{
    public sealed class GetReportByCapabilityIdentifierCommand : ICommand<IEnumerable<ReportRoot>>
    {
        [JsonPropertyName("capabilityIdentifier")]
        public string CapabilityIdentifier { get; init; }

        [JsonConstructor]
        public GetReportByCapabilityIdentifierCommand(string capabilityIdentifier)
        {
            CapabilityIdentifier = capabilityIdentifier;
        }
    }
}