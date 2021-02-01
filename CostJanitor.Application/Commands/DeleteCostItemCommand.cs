using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Commands;

namespace CostJanitor.Application.Commands
{
    public sealed class DeleteCostItemCommand : ICommand<bool>
    {
        [JsonPropertyName("costItemId")]
        public Guid CostItemId { get; init; }

        [JsonConstructor]
        public DeleteCostItemCommand(Guid costItemId)
        {
            CostItemId = costItemId;
        }

        public DeleteCostItemCommand()
        {
            
        }
    }
}