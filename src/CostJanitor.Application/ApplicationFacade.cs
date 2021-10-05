using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Facade;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacade : Facade, IApplicationFacade
    {
        public ApplicationFacade(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }

        public override async Task<T> Execute<T>(ICommand<T> command, CancellationToken cancellationToken = default)
        {
            //Read: https://medium.com/swlh/dependency-injection-v-mediatr-a-simple-c-benchmark-32630ff864ea
            //This is breaking API (but needed by the .NET Core App). Consider how to solve it and refactor abstractions class
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            return await mediator.Send(command, cancellationToken);
        }
    }
}