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
        Task<IEnumerable<ReportRoot>> GetReportByCapabilityIdentifierAsync(string capabilityIdentifier, CancellationToken ct = default);

        Task<ReportRoot> AddReportAsync(IEnumerable<CostItem> costItems, CancellationToken ct = default);

        Task<ReportRoot> UpdateReportAsync(ReportRoot report, CancellationToken ct = default);

        Task<bool> DeleteReportAsync(Guid reportItemId, CancellationToken ct = default);

        Task<CostItem> AddOrUpdateCostItemAsync(Guid reportItemId, string capabilityIdentifier, string label, string value, CancellationToken ct = default);
        
        Task<bool> DeleteCostItemAsync(Guid reportItemId, string label, string capabilityIdentifier = default, CancellationToken ct = default);
    }
}