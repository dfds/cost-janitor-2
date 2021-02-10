using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application.Services
{
    public class CostService : ICostService
    {
        private readonly IReportRepository _reportRepository;

        public CostService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        
        public async Task<IEnumerable<ReportRoot>> GetReportByCapabilityIdentifierAsync(string capabilityIdentifier, CancellationToken ct = default)
        {
            var reportRoots = await _reportRepository.GetAsync(r => r.CostItems.Any(ci => ci.CapabilityIdentifier == capabilityIdentifier));

            return reportRoots;
        }

        public async Task<ReportRoot> AddReportAsync(IEnumerable<CostItem> costItems, CancellationToken ct = default)
        {
            var reportRoot = new ReportRoot();

            reportRoot.AddCostItem(costItems);

            reportRoot = _reportRepository.Add(reportRoot);
            
            await _reportRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return reportRoot;
        }

        public async Task<ReportRoot> UpdateReportAsync(ReportRoot report, CancellationToken ct = default)
        {
            var reportRoot = _reportRepository.Update(report);
            
            await _reportRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return reportRoot;
        }

        public async Task<CostItem> AddOrUpdateCostItemAsync(Guid reportItemId, string capabilityId, string label, string value, CancellationToken ct = default)
        {
            var costItem = new CostItem(label, value, capabilityId);
            var reportRoot = await _reportRepository.GetAsync(reportItemId);

            if (reportRoot == null) return costItem;

            if (reportRoot.CostItems.Any(ci => ci.Equals(costItem)))
            {
                reportRoot.RemoveCostItem(reportRoot.CostItems.Single(ci => ci.Equals(costItem)));
            }

            reportRoot.AddCostItem(costItem);

            await UpdateReportAsync(reportRoot, ct);

            return costItem;
        }

        public async Task<bool> DeleteReportAsync(Guid id, CancellationToken ct = default)
        {
            var reportRoot = await _reportRepository.GetAsync(id);

            _reportRepository.Delete(reportRoot);

            await _reportRepository.UnitOfWork.SaveEntitiesAsync(ct);

            return true;
        }

        public async Task<bool> DeleteCostItemAsync(Guid reportItemId, string label, string capabilityIdentifier = default, CancellationToken ct = default)
        {
            var reportRoot = await _reportRepository.GetAsync(reportItemId);
            var query = reportRoot.CostItems.Where(ci => ci.Label == label).AsQueryable();

            if (!string.IsNullOrEmpty(capabilityIdentifier))
            {
                query = query.Where(ci => ci.CapabilityIdentifier == capabilityIdentifier);
            }

            foreach (var ci in query.AsEnumerable())
            {
                reportRoot.RemoveCostItem(ci);
            }
            
            await UpdateReportAsync(reportRoot, ct);

            return true;
        }
    }
}