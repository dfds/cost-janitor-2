using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class GetReportByCapabilityIdentifierCommand : ICommand<IEnumerable<ReportItem>>
    {
        [JsonPropertyName("identifier")]
        public String Identifier { get; init; }

        [JsonConstructor]
        public GetReportByCapabilityIdentifierCommand(String id)
        {
            Identifier = id;
        }
    }
}