using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;
using CostJanitor.Domain.ValueObjects;

namespace CostJanitor.Application.Services
{
    public class CostService : ICostService
    {
        private readonly IReportItemRepository _reportItemRepository;

        public CostService(IReportItemRepository reportItemRepository)
        {
            _reportItemRepository = reportItemRepository;
        }
        
        public async Task<IEnumerable<ReportItem>> GetReportByCapabilityIdentifier(string identifier, CancellationToken ct = default)
        {
            var reportItems = await _reportItemRepository.GetAsync(i => true);
            return reportItems
                .Where(i => i.CostItems.Any(i => i.CapabilityIdentifier == identifier));
        }

        public Task<ReportItem> CreateOrAddReport(Guid id, IEnumerable<CostItem> costItems, CancellationToken ct = default)
        {
            var reportItem = new ReportItem(id);
            reportItem.AddCostItem(costItems);
            return Task.FromResult(_reportItemRepository.Add(reportItem));
        }

        public async Task<CostItem> CreateOrAddCostItem(string capabilityId, string label, string value, Guid reportItemId, CancellationToken ct = default)
        {
            var costItem = new CostItem(label, value, capabilityId);
            var reportItem = await _reportItemRepository.GetAsync(reportItemId);
            reportItem.AddCostItem(costItem);
            return costItem;
        }

        public async Task<bool> DeleteReport(Guid id, CancellationToken ct = default)
        {
            var reportItem = await _reportItemRepository.GetAsync(id);
            _reportItemRepository.Delete(reportItem);
            return true;
        }

        public async Task<bool> DeleteCostItem(Guid id, Guid reportItemId, CancellationToken ct = default)
        {
            var reportItem = await _reportItemRepository.GetAsync(reportItemId);
            throw new NotImplementedException();
            return true;
        }
    }
}