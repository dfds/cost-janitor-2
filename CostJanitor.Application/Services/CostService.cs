using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Repositories;
using CostJanitor.Domain.Services;

namespace CostJanitor.Application.Services
{
    public class CostService : ICostService
    {
        private readonly ICostItemRepository _costItemRepository;
        private readonly IReportItemRepository _reportItemRepository;

        public CostService(ICostItemRepository costItemRepository, IReportItemRepository reportItemRepository)
        {
            _costItemRepository = costItemRepository;
            _reportItemRepository = reportItemRepository;
        }
        
        public async Task<IEnumerable<ReportItem>> GetReportByCapabilityIdentifier(string identifier, CancellationToken ct = default)
        {
            var reportItems = await _reportItemRepository.GetAsync(i => true);
            return reportItems
                .Where(i => i.CostItemReferences.Any(i => i.CapabilityIdentifier == identifier));
        }

        public Task<ReportItem> CreateOrAddReport(Guid id, IEnumerable<CostItem> costItems, CancellationToken ct = default)
        {
            var reportItem = new ReportItem(id);
            reportItem.AddCostItem(costItems);
            return Task.FromResult(_reportItemRepository.Add(reportItem));
        }

        public Task<CostItem> CreateOrAddCostItem(string capabilityId, string label, string value, CancellationToken ct = default)
        {
            var costItem = new CostItem(label, value, capabilityId);
            return Task.FromResult(_costItemRepository.Add(costItem));
        }

        public async Task<bool> DeleteReport(Guid id, CancellationToken ct = default)
        {
            var reportItem = await _reportItemRepository.GetAsync(id);
            _reportItemRepository.Delete(reportItem);
            return true;
        }

        public async Task<bool> DeleteCostItem(Guid id, CancellationToken ct = default)
        {
            var costItem = await _costItemRepository.GetAsync(id);
            _costItemRepository.Delete(costItem);
            return true;
        }
    }
}