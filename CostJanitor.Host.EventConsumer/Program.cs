using CostJanitor.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CostJanitor.Host.EventConsumer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<AwsAccountEventWorker>();

            DependencyInjection.AddApplication(services);
        })
        .ConfigureLogging(logBuilder =>
        {
            logBuilder.AddSentry();
        });
    }
}