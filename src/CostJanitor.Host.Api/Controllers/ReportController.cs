using CostJanitor.Application;
using CostJanitor.Application.Commands.Report;
using CostJanitor.Domain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostJanitor.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("dfds.all.read")]
    public class ReportController
    {
        private readonly IApplicationFacade _applicationFacade;

        public ReportController(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade;
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
            return await _applicationFacade.Execute(command);
        }

        [HttpPost]
        public async Task<ReportRoot> Create(CreateReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpPut]
        public async Task<ReportRoot> Update(UpdateReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteReportCommand command)
        {
            return await _applicationFacade.Execute(command);
        }
    }
}