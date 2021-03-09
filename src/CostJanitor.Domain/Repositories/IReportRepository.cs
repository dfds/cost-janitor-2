using CloudEngineering.CodeOps.Abstractions.Repositories;
using CostJanitor.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace CostJanitor.Domain.Repositories
{
    public interface IReportRepository : IRepository<ReportRoot>
    {
        Task<ReportRoot> GetAsync(Guid reportItemId);
    }
}