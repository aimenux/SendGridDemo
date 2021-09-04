using System;
using System.IO;
using System.Threading.Tasks;
using App.Configuration;
using App.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

namespace App
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                var examples = host.Services.GetServices<IExample>();
                foreach (var example in examples)
                {
                    await example.RunAsync();
                }
            }

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IExample, Example1>();
                    services.AddTransient<IExample, Example2>();
                    services.AddTransient<IExample, Example3>();
                    services.AddTransient<IExample, Example4>();
                    services.AddTransient<IExample, Example5>();
                    services.AddTransient<IExample, Example6>();
                    services.AddTransient<IExample, Example7>();
                    services.AddTransient<IExample, Example8>();
                    services.AddTransient<IExample, Example9>();
                    services.AddSendGrid((serviceProvider, options) =>
                    {
                        var settings = serviceProvider.GetRequiredService<IOptions<Settings>>().Value;
                        options.ApiKey = settings.ApiKey;
                    });
                    services.Configure<Settings>(context.Configuration.GetSection(nameof(Settings)));
                })
                .ConfigureLogging((_, loggingBuilder) =>
                {
                    loggingBuilder.AddNonGenericLogger();
                })
                .UseConsoleLifetime();

        private static void AddNonGenericLogger(this ILoggingBuilder loggingBuilder)
        {
            var categoryName = typeof(Program).Namespace;
            var services = loggingBuilder.Services;
            services.AddSingleton(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                return loggerFactory.CreateLogger(categoryName);
            });
        }
    }
}
