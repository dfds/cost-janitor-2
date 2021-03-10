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
        private bool _disposedValue;

        public AwsCostClient(IAwsFacade awsFacade)
        {
            _awsFacade = awsFacade;
        }

        public async Task<IEnumerable<CostDto>> GetMonthlyTotalCostAllAccountsAsync()
        {
            var result = await _awsFacade.Execute(new GetMonthlyTotalCostCommand());

            return result;
        }

        public async Task<CostDto> GetMonthlyTotalCostByAccountIdAsync(string accountId)
        {
            var result = (await _awsFacade.Execute(new GetMonthlyTotalCostCommand(accountId))).FirstOrDefault();

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _awsFacade.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);

            System.GC.SuppressFinalize(this);
        }
    }
}