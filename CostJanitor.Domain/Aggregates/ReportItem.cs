using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public sealed class ReportItem : Entity<Guid>, IAggregateRoot
    {
        private List<CostItemReference> _costItemReferences;
        public IEnumerable<CostItemReference> CostItemReferences => _costItemReferences.AsReadOnly();

        public ReportItem(Guid id)
        {
            this.Id = id;
        }

        private ReportItem()
        {
            _costItemReferences = new List<CostItemReference>();
        }

        public void AddCostItem(Guid costItemId)
        {
            _costItemReferences.Add(new CostItemReference(costItemId));
        }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}