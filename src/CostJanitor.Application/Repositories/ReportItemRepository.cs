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
    public class ReportItemRepository : EntityFrameworkRepository<ReportItem>, IReportItemRepository
    {
        public ReportItemRepository(DomainContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<ReportItem>> GetAsync(Expression<Func<ReportItem, bool>> filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                var x = _context.ReportItems.AsQueryable();
                    x = x.AsNoTracking();
                    x = x.Where(filter);
                    x = x.Include(i => i.CostItems);
                    return x.AsEnumerable();
            });
        }

        public async Task<ReportItem> GetAsync(Guid reportItemId)
        {
            var reportItem = await _context.ReportItems.FindAsync(reportItemId);

            if (reportItem != null)
            {
                var entry = _context.Entry(reportItem);

                if (entry != null)
                {
                    await entry.Reference(i => i.CostItems).LoadAsync();
                }
            }

            return reportItem;
        }
    }
}