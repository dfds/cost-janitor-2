using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CostJanitor.Domain.Events;
using CostJanitor.Domain.Events.Cost;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;

namespace CostJanitor.Domain.Aggregates
{
    public sealed class CostItem : Entity<Guid>, IAggregateRoot
    {
        public string Label { get; init; }
        public string Value { get; init; }
        
        public string CapabilityIdentifier { get; init; }
        
        public CostItem(string label, string value, string capabilityIdentifier) : this()
        {
            this.Label = label;
            this.Value = value;
            this.CapabilityIdentifier = capabilityIdentifier;
        }

        private CostItem()
        {
            var evt = new CostItemCreatedEvent(this);
            AddDomainEvent(evt);
        }

        public void SetId(Guid val)
        {
            Id = val;
            AddDomainEvent(new CostItemIdChangedEvent(this));
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

            if (string.IsNullOrEmpty(CapabilityIdentifier))
            {
                result.Add(new ValidationResult(nameof(CapabilityIdentifier)));
            }

            if (this.Id == Guid.Empty)
            {
                result.Add(new ValidationResult(nameof(this.Id)));
            }

            return result;
        }
    }
}