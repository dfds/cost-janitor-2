using CloudEngineering.CodeOps.Infrastructure.AmazonWebServices;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CostJanitor.Infrastructure.IntegrationTest.Fixtures
{
    public class ServiceProviderFixture : IDisposable
    {
        private readonly ConfigurationFixture _configFixture = new ConfigurationFixture();

        public IServiceProvider Provider { get; init; }

        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();

            services.AddAmazonWebServices(_configFixture.Configuration);

            Provider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            _configFixture.Dispose();
        }
    }
}
