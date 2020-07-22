using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class OrderServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;

        public OrderServiceTests()
        {
            _productService = new ProductService();
            _campaignService = new CampaignService(_productService);
            _orderService = new OrderService(_productService, _campaignService);
        }

        [Fact]
        public void Create_Order_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => _orderService.CreateOrder("create_order P11 1asd0"));
        }
        [Fact]
        public void Order_Should_Be_Created()
        {
            CreateProductCampaignAndOrder();
            var order = _orderService.GetOrder();

           Assert.Equal( 10, order.Quantity);
           Assert.Equal("P11",order.ProductCode);
        }
        [Fact]
        public void OrderQuantityInPerPeriodOfCampaign_Should_Be_Set_When_Campaign_Exist()
        {
            CreateProductCampaignAndOrder();

            var expectedOrderQuantityInPerPeriodOfCampaign = 10;

            Assert.Equal(expectedOrderQuantityInPerPeriodOfCampaign, _orderService.GetOrderQuantityInPerPeriodOfCampaign());
          
        }
        [Fact]
        public void OrderQuantityInPerPeriodOfCampaign_Should_Be_Set_Zero()
        {
            CreateProductCampaignAndOrder();

            var expectedOrderQuantityInPerPeriodOfCampaign = 0;

            _orderService.SetZeroOrderQuantityInPerPeriodOfCampaign();
          
            Assert.Equal(expectedOrderQuantityInPerPeriodOfCampaign, _orderService.GetOrderQuantityInPerPeriodOfCampaign());
        }
        private void CreateProductCampaignAndOrder()
        {
            _productService.CreateProduct("create_product P11 100 1000");
            _campaignService.CreateCampaign("create_campaign C11 P11 10 20 100");
            _orderService.CreateOrder("create_order P11 10");
        }

    }

}

