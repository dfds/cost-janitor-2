using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Infrastructure.EntityFramework;
using CostJanitor.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CostJanitor.Application.Repositories
{
    public class CostItemRepository : EntityFrameworkRepository<CostItem>, ICostItemRepository
    {
        public CostItemRepository(DomainContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<CostItem>> GetAsync(Expression<Func<CostItem, bool>> filter)
        {
            return await Task.Factory.StartNew(() => _context.CostItems
                .AsNoTracking()
                .Where(filter)
                .AsEnumerable());
        }

        public async Task<CostItem> GetAsync(int costItemId)
        {
            return await _context.CostItems.FindAsync(costItemId);
        }
    }
}