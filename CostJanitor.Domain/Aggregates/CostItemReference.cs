using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public class CostItemReference : Entity<Guid>
    {
        public string CapabilityIdentifier { get; init; }
        public DateTime Added { get; init; }
        
        public CostItemReference(string capabilityIdentifier)
        {
            CapabilityIdentifier = capabilityIdentifier;
            Added = DateTime.Now;
        }
        
        private CostItemReference() {}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}