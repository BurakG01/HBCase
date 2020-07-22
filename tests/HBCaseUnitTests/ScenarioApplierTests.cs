using System.Collections.Generic;
using System.IO;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using HBCase.Enums;
using HBCase.Scenario;
using Xunit;

namespace HBCaseUnitTests
{
    public class ScenarioApplierTests
    {
        private readonly IScenarioApplier _scenarioApplier;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;
        private readonly IOrderService _orderService;

        public ScenarioApplierTests()
        {
            _productService = new ProductService();
            _campaignService = new CampaignService(_productService);
            _orderService = new OrderService(_productService, _campaignService);
            _scenarioApplier = new ScenarioApplier(_productService, _orderService, _campaignService);
        }

        [Fact]
        public void Applier_Should_Throw_Exception_When_Invalid_Command_Arrived()
        {
            var commands = new List<string>
            {
                "create_product P11 100 1000",
                "get_product_info P11",
                "blabla"
            };

            Assert.Throws<InvalidDataException>(() => _scenarioApplier.ApplyCommands(commands));
        }
        [Fact]
        public void Applier_Should_Apply_All_Commands_Correctly()
        {
            var commands = new List<string>
            {
                "create_product P4 100 1000",
                "create_campaign C1 P4 5 40 100",
                "increase_time 1",
                "create_order P4 5",
                "increase_time 1",
                "create_order P4 25",
                "increase_time 1",
                "increase_time 1",
                "get_product_info P4",
                "increase_time 2",
                "create_order P4 10",
                "get_campaign_info C1",
                "create_order P4 50",
                "increase_time 2"
            };
            _scenarioApplier.ApplyCommands(commands);

            var product = _productService.GetProduct();

            var campaign = _campaignService.GetCampaign();

            var expectedProductPrice = 90m;
            var expectedProductStock = 910;
            var expectedCampaignStatus = Status.Passive;
            var expectedCampaignTurnover = 2725;
            var expectedCampaignAverageItemPrice = 90.833m;

            Assert.Equal(expectedProductPrice,product.Price);

            Assert.Equal(expectedProductStock,product.Stock);

            Assert.Equal(expectedCampaignStatus,campaign.Status);

            Assert.Equal(expectedCampaignTurnover, campaign.Turnover);

            Assert.Equal(expectedCampaignAverageItemPrice, campaign.AverageItemPrice);

        }
    }
}
