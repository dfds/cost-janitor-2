using CloudEngineering.CodeOps.Abstractions.Facade;
using MediatR;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacade : Facade, IApplicationFacade
    {
        public ApplicationFacade(IMediator mediator) : base(mediator)
        {
        }
    }
}