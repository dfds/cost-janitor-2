using Microsoft.Extensions.DependencyInjection;
using System;

namespace CostJanitor.Application.IntegrationTest.Fixtures
{
    public class ServiceProviderFixture : IDisposable
    {
        private readonly ConfigurationFixture _configFixture = new ConfigurationFixture();

        public IServiceProvider Provider { get; init; }

        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();

            services.AddApplication(_configFixture.Configuration);

            Provider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            _configFixture.Dispose();
        }
    }
}
