using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using HBCase.Enums;
using Xunit;

namespace HBCaseUnitTests
{
    public class CampaignServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;

        public CampaignServiceTests()
        {
            _productService = new ProductService();
            _campaignService = new CampaignService(_productService);
            _orderService = new OrderService(_productService, _campaignService);
        }

        [Fact]
        public void Create_Campaign_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => _campaignService.CreateCampaign("create_campaign C1 P4 5 20 sds"));
        }
        [Fact]
        public void Campaign_Should_Be_Created()
        {
            CreateProductCampaignAndOrder();
            var campaign = _campaignService.GetCampaign();
            Assert.Equal("C11",campaign.Name);
            Assert.Equal("P11",campaign.ProductCode);
            Assert.Equal(10,campaign.Duration);
            Assert.Equal(20,campaign.PriceManipulationLimit);
            Assert.Equal(100,campaign.TargetSalesCount);

        }
        [Fact]
        public void Campaign_TotalSalesCount_And_Turnover_Should_Be_Increased_When_Order_Created()
        {
            CreateProductCampaignAndOrder();
            var campaign = _campaignService.GetCampaign();
            Assert.Equal(10,campaign.TotalSalesCount);
            Assert.Equal(1000,campaign.Turnover);
        }
        [Fact]
        public void Campaign_AverageItemPrice_Should_Be_Set_When_Order_Created()
        {
            CreateProductCampaignAndOrder();
            var campaign = _campaignService.GetCampaign();
            Assert.Equal(100, campaign.AverageItemPrice);
           
        }
        [Fact]
        public void Campaign_Status_Should_Be_Passive_After_Many_Increase_Time_Command_Arrived()
        {
            CreateProductCampaignAndOrder();

            for (int i = 0; i < 10; i++)
            {
                _campaignService.DecreaseCampaignDuration(1);
            }
            var campaign = _campaignService.GetCampaign();
            Assert.Equal(Status.Passive,campaign.Status);

        }
        private void CreateProductCampaignAndOrder()
        {
            _productService.CreateProduct("create_product P11 100 1000");
            _campaignService.CreateCampaign("create_campaign C11 P11 10 20 100");
            _orderService.CreateOrder("create_order P11 10");
        }
    }
}
