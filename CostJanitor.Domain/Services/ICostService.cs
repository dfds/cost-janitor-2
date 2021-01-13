using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Services;

namespace CostJanitor.Domain.Services
{
    public interface ICostService : IDomainService
    {
        Task<IEnumerable<ReportItem>> GetReportByCapabilityIdentifier(string identifier, CancellationToken ct = default);
        Task<ReportItem> CreateOrAddReport(Guid id, IEnumerable<CostItem> costItems, CancellationToken ct = default);
        Task<CostItem> CreateOrAddCostItem(Guid id, string label, string value, CancellationToken ct = default);
        Task<bool> DeleteReport(Guid id, CancellationToken ct = default);
        Task<bool> DeleteCostItem(Guid id, CancellationToken ct = default);
    }
}