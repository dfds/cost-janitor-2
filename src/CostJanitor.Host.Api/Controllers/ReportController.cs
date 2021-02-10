using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CostJanitor.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {
        private readonly ILogger<ReportController> _logger;
        private ICostService _costService;

        public ReportController(ILogger<ReportController> logger, ICostService costService)
        {
            _logger = logger;
            _costService = costService;
        }

        [HttpGet]
        public async Task<IEnumerable<ReportItem>> Get(string capabilityIdentifier)
        {
            return await _costService.GetReportByCapabilityIdentifier(capabilityIdentifier);
        }

        [HttpPost]
        public async Task<ReportItem> Create(ReportItem reportItem)
        {
            throw new NotImplementedException();
        }
    }
}