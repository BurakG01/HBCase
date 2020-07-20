﻿using System;
using HBCase.Scenario;
using HBCase.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HBCase
{
    class Program
    {
        static void Main(string[] args)
        {

            var scenarioOperation = InitializeServices().GetService<IScenarioReader>();

            scenarioOperation.ReadScenarios();

            Console.ReadLine();



        }

        private static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection()
                .AddScoped<IProductService,ProductService>()
                .AddScoped<ICampaignService,CampaignService>()
                .AddScoped<IOrderService,OrderService>()
                .AddTransient<IScenarioReader,ScenarioReader>()
                .BuildServiceProvider();
            return services;
        }
    }
}