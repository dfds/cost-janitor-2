using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Commands;

namespace CostJanitor.Application.Commands
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