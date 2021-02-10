using System;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Repositories;

namespace CostJanitor.Domain.Repositories
{
    public interface IReportItemRepository : IRepository<ReportItem>
    {
        Task<ReportItem> GetAsync(Guid reportItemId);
    }
}