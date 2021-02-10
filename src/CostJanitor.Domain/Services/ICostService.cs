using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using ResourceProvisioning.Abstractions.Services;

namespace CostJanitor.Domain.Services
{
    public interface ICostService : IService
    {
        Task<IEnumerable<ReportItem>> GetReportByCapabilityIdentifier(string identifier, CancellationToken ct = default);
        Task<ReportItem> CreateOrAddReport(Guid id, IEnumerable<CostItem> costItems, CancellationToken ct = default);
        Task<CostItem> CreateOrAddCostItem(string capabilityId, string label, string value, Guid reportItemId, CancellationToken ct = default);
        Task<bool> DeleteReport(Guid id, CancellationToken ct = default);
        Task<bool> DeleteCostItem(Guid id, Guid reportItemId, CancellationToken ct = default);
    }
}