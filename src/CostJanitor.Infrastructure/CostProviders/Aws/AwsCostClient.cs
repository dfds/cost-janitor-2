using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.Commands.Cost;
using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices.DataTransferObjects.Cost;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostJanitor.Infrastructure.CostProviders.Aws
{
    public class AwsCostClient : IAwsCostClient
    {
        private readonly IAwsFacade _awsFacade;

        public AwsCostClient(IAwsFacade awsFacade)
        {
            _awsFacade = awsFacade;
        }

        public async Task<IEnumerable<CostDto>> GetMonthlyTotalCostAllAccountsAsync()
        {
            _awsFacade.Connect();

            var result = await _awsFacade.Execute(new GetMonthlyTotalCostCommand());

            _awsFacade.Disconnect();

            return result;
        }

        public async Task<CostDto> GetMonthlyTotalCostByAccountIdAsync(string accountId)
        {
            _awsFacade.Connect();

            var result = (await _awsFacade.Execute(new GetMonthlyTotalCostCommand(accountId))).FirstOrDefault();

            _awsFacade.Disconnect();

            return result;
        }
    }
}