using CloudEngineering.CodeOps.Abstractions.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacade : Facade, IApplicationFacade
    {
        public ApplicationFacade(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }
    }
}