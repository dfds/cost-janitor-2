using System;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Repositories;

namespace CostJanitor.Domain.Repositories
{
    public interface IReportRepository : IRepository<ReportRoot>
    {
        Task<ReportRoot> GetAsync(Guid reportItemId);
    }
}