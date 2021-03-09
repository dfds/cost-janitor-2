using CloudEngineering.CodeOps.Abstractions.Commands;
using CloudEngineering.CodeOps.Abstractions.Facade;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CostJanitor.Application
{
    public sealed class ApplicationFacade : IFacade
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ApplicationFacade> _logger;

        public ApplicationFacade(IMediator mediator, ILogger<ApplicationFacade> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> Execute<T>(ICommand<T> command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Processing {command.GetType().FullName}");

            return await _mediator.Send(command, cancellationToken);
        }
    }
}