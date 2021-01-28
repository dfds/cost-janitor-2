using System;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using ResourceProvisioning.Abstractions.Repositories;

namespace CostJanitor.Domain.Repositories
{
    public interface ICostItemRepository : IRepository<CostItem>
    {
        Task<CostItem> GetAsync(Guid costItemId);
    }
}