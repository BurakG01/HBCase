using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using HBCase.Domain.Interfaces;
using HBCase.Enums;

namespace HBCase.Scenario
{
    public interface IScenarioApplier
    {
        void ApplyCommands(List<string> commands);
    }

    public class ScenarioApplier : IScenarioApplier
    {
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;
        private readonly IOrderService _orderService;
        public ScenarioApplier(IProductService productService, IOrderService orderService, ICampaignService campaignService)
        {
            _productService = productService;
            _orderService = orderService;
            _campaignService = campaignService;
        }
        public void ApplyCommands(List<string> commands)
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

                        Console.WriteLine(command);

                        var hour = int.Parse(commandProperties[1]);

                        var product = _productService.GetProduct();

                        if (_campaignService.IsCampaignExist(product.ProductCode))
                        {
                            _campaignService.DecreaseCampaignDuration(hour);

                            var campaignStatus = _campaignService.GetCampaign().Status;

                            if (campaignStatus == Status.Active)
                            {
                                var orderQuantityInPerPeriodOfCampaign = _orderService.GetOrderQuantityInPerPeriodOfCampaign();

                                var targetSalesCountInHour = _campaignService.GetTargetSalesCountInHour();

                                var orderCountInCampaignPeriod = orderQuantityInPerPeriodOfCampaign / hour;

                                _productService.TryToChangeProductPrice(orderCountInCampaignPeriod, targetSalesCountInHour);
                            }
                        }
                        _orderService.SetZeroOrderQuantityInPerPeriodOfCampaign();

                        SleepScreen();
                        break;

                    default:
                        throw new InvalidDataException("Invalid Command");
                }

            }

        }
        private void SleepScreen()
        {
            Thread.Sleep(100);
        }
    }

}
