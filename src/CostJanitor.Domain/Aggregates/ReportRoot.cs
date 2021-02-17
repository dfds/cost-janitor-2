using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CostJanitor.Domain.Events.Report;
using CostJanitor.Domain.ValueObjects;
using CloudEngineering.CodeOps.Abstractions.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public sealed class ReportRoot : Entity<Guid>, IAggregateRoot
    {
        private readonly List<CostItem> _costItems;

        public IEnumerable<CostItem> CostItems => _costItems.AsReadOnly();
        
        public ReportRoot()
        {
            _costItems = new List<CostItem>();

            var evt = new ReportCreatedEvent(this);
            
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
            _costItems.Remove(costItem);
        }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}