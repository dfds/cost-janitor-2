using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class DeleteReportCommand : ICommand<bool>
    {
        [JsonPropertyName("reportId")]
        public Guid ReportId { get; init; }

        [JsonConstructor]
        public DeleteReportCommand(Guid reportId)
        {
            ReportId = reportId;
        }
    }
}