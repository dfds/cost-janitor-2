using CostJanitor.Application.Commands;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ReportRoot>> Get(string capabilityIdentifier)
        {
            var command = new GetReportByCapabilityIdentifierCommand(capabilityIdentifier);

            return await Get(command);
        }

        [HttpGet("command")]
        public async Task<IEnumerable<ReportRoot>> Get(GetReportByCapabilityIdentifierCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ReportRoot> Create(CreateReportCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<ReportRoot> Update(UpdateReportCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteReportCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}