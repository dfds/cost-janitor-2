using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CostJanitor.Domain.Events.Report;
using CostJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public sealed class ReportItem : Entity<Guid>, IAggregateRoot
    {
        private readonly List<CostItem> _costItems;

        public IEnumerable<CostItem> CostItems => _costItems.AsReadOnly();

        public ReportItem(Guid id) : this()
        {
            this.Id = id;
        }

        private ReportItem()
        {
            _costItems = new List<CostItem>();
            var evt = new ReportItemCreatedEvent(this);
            AddDomainEvent(evt);
        }

        public void AddCostItem(CostItem costItem)
        {
            _costItems.Add(costItem);
        }

        public void AddCostItem(IEnumerable<CostItem> costItems)
        {
            _costItems.AddRange(costItems);
        }

        public void RemoveCostItem(CostItem costItem)
        {
            throw new NotImplementedException();
        }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}