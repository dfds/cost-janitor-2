using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CostJanitor.Domain.Events;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public sealed class CostItem : Entity<Guid>, IAggregateRoot
    {
        public string Label { get; init; }
        public string Value { get; init; }
        
        public CostItem(string label, string value, Guid id)
        {
            this.Label = label;
            this.Value = value;
            this.Id = id;
        }

        private CostItem()
        {
            var evt = new CostItemCreatedEvent(this);
            AddDomainEvent(evt);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            
            if (string.IsNullOrEmpty(this.Label))
            {
                result.Add(new ValidationResult(nameof(this.Label)));
            }

            if (string.IsNullOrEmpty(this.Value))
            {
                result.Add(new ValidationResult(nameof(this.Value)));
            }

            if (this.Id == Guid.Empty)
            {
                result.Add(new ValidationResult(nameof(this.Id)));
            }

            return result;
        }
    }
}