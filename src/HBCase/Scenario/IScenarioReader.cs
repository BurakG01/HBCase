using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using HBCase.Services;

namespace HBCase.Scenario
{
    public interface IScenarioReader
    {
        void ReadScenarios();
    }

    public class ScenarioReader : IScenarioReader
    {
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;
        private readonly IOrderService _orderService;

        public ScenarioReader(IProductService productService, ICampaignService campaignService, IOrderService orderService)
        {
            _productService = productService;
            _campaignService = campaignService;
            _orderService = orderService;
        }
        public void ReadScenarios()
        {
            var filePaths = new string[] { Directory.GetCurrentDirectory() + @"\Scenarios\Scenario1.txt" ,
                Directory.GetCurrentDirectory() + @"\Scenarios\Scenario2.txt" ,
                Directory.GetCurrentDirectory() + @"\Scenarios\Scenario3.txt" ,
                Directory.GetCurrentDirectory() + @"\Scenarios\Scenario4.txt" ,
                Directory.GetCurrentDirectory() + @"\Scenarios\Scenario5.txt" };

            foreach (var filePath in filePaths)
            {
                ApplyScenario(filePath);
            }
        }

        private void ApplyScenario(string filePath)
        {
            var fileName = filePath.Substring(filePath.Length - 13);

            Console.WriteLine($" Output of {fileName} File");

            ReadCommandsFromFile(filePath);

            Console.WriteLine("\n");
        }
        private void ReadCommandsFromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            StreamReader sw = new StreamReader(fs);
            var commands = new List<string>();
            string commandLine = sw.ReadLine();

            while (commandLine != null)
            {
                commands.Add(commandLine);

                commandLine = sw.ReadLine();
            }

            ApplyCommands(commands);

            fs.Close();
        }
        private void ApplyCommands(List<string> commands)
        {
            foreach (var command in commands)
            {
                var commandProperties = command.Split(" ");
                var commandName = commandProperties.First();

                switch (commandName)
                {
                    case "create_product":

                        _productService.CreateProduct(command);
                        SleepScreen();
                        break;

                    case "create_campaign":

                        _campaignService.CreateCampaign(command);
                        SleepScreen();
                        break;

                    case "get_product_info":
                        // Generic bir control yazılabilir her bir command için
                        if (commandProperties.Length > 1)
                        {
                            var productCode = commandProperties[1];
                            _productService.GetProductInfo(productCode);
                        }
                        SleepScreen();
                        break;

                    case "create_order":
                        _orderService.CreateOrder(command);
                        SleepScreen();

                        break;

                    case "get_campaign_info":

                        _campaignService.GetCampaignInfo();
                        SleepScreen();
                        break;

                    case "increase_time":

                        var hour = int.Parse(commandProperties[1]);

                        var product = _productService.GetProduct();

                        if (_campaignService.IsCampaignExist(product.ProductCode))
                        {
                            var orderQuantityInPerPeriodOfCampaign = _orderService.GetOrderQuantityInPerPeriodOfCampaign();

                            var targetSalesCountInHour = _campaignService.GetTargetSalesCountInHour();

                            var orderCountInCampaignPeriod = orderQuantityInPerPeriodOfCampaign / hour;

                            _productService.TryToChangeProductPrice(orderCountInCampaignPeriod, targetSalesCountInHour);

                            _campaignService.DecreaseCampaignDuration(hour);

                            _orderService.SetZeroOrderQuantityInPerPeriodOfCampaign();
                        }
                        SleepScreen();
                        break;
                }

            }

        }


        private void SleepScreen()
        {
            Thread.Sleep(100);
        }
    }
}
