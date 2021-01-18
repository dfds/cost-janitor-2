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

        public Task<CostItem> CreateOrAddCostItem(Guid id, string label, string value, CancellationToken ct = default)
        {
            var costItem = new CostItem(label, value, id);
            return Task.FromResult(_costItemRepository.Add(costItem));
        }

        public async Task<bool> DeleteReport(Guid id, CancellationToken ct = default)
        {
            var reportItems = await _reportItemRepository.GetAsync(i => i.Id == id);
            _reportItemRepository.Delete(reportItems.First());
            return true;
        }

        public async Task<bool> DeleteCostItem(Guid id, CancellationToken ct = default)
        {
            var costItems = await _costItemRepository.GetAsync(i => i.Id == id);
            _costItemRepository.Delete(costItems.First());
            return true;
        }
    }
}