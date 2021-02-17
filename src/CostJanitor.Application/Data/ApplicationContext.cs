using CloudEngineering.CodeOps.Abstractions.Data;
using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CostJanitor.Application.Data
{
    public class ApplicationContext : EntityContext
    {
        public virtual DbSet<CostItem> CostItems { get; set; }

        public virtual DbSet<ReportRoot> ReportItems { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions options, IMediator mediator = default, IDictionary<Type, IEnumerable<IView>> seedData = default) : base(options)
        {
            
        }
    }
}
