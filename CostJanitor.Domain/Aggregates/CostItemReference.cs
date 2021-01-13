using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public class CostItemReference : Entity<Guid>
    {
        public DateTime Added { get; init; }
        
        public CostItemReference(Guid costItemId)
        {
            this.Id = costItemId;
            this.Added = DateTime.Now;
        }
        
        private CostItemReference() {}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}