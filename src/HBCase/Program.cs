using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using HBCase.Scenario;
using Microsoft.Extensions.DependencyInjection;

namespace HBCase
{
    class Program
    {
        static void Main(string[] args)
        {
            var scenarioOperation = InitializeServices().GetService<IScenarioOperations>();

            scenarioOperation.StartScenarios();

            Console.ReadLine();
        }

        private static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection()
                .AddScoped<IProductService,ProductService>()
                .AddScoped<ICampaignService,CampaignService>()
                .AddScoped<IOrderService,OrderService>()
                .AddTransient<IScenarioReader,ScenarioReader>()
                .AddTransient<IScenarioApplier, ScenarioApplier>()
                .AddTransient<IScenarioOperations, ScenarioOperations>()
                .BuildServiceProvider();
            return services;
        }
    }
}
