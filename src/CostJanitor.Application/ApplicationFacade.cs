using CloudEngineering.CodeOps.Abstractions.Facade;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacade : Facade, IApplicationFacade
    {
        public ApplicationFacade(IMediator mediator, ILogger<ApplicationFacade> logger = default) : base(mediator, logger)
        {
        }
    }
}