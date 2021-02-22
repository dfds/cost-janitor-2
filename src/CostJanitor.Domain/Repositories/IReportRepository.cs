using System;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CloudEngineering.CodeOps.Abstractions.Repositories;

namespace CostJanitor.Domain.Repositories
{
    public interface IReportRepository : IRepository<ReportRoot>
    {
        Task<ReportRoot> GetAsync(Guid reportItemId);
    }
}