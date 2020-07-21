using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class OrderServiceTests
    {
        public readonly IOrderService OrderService;
        public readonly IProductService ProductService;
        public readonly ICampaignService CampaignService;

        public OrderServiceTests()
        {
            ProductService = new ProductService();
            CampaignService = new CampaignService(ProductService);
            OrderService = new OrderService(ProductService, CampaignService);
        }
        [Fact]
        public void CreateOrder_With_Invalid_Properties_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => OrderService.CreateOrder("create_order P11 1asd0"));
        }

        [Fact]
        public void CreateOrder_With_Valid_Properties()
        {
            CreateProductCampaignAndOrder();
            var order = OrderService.GetOrder();

           Assert.Equal( 10, order.Quantity);
           Assert.Equal("P11",order.ProductCode);
        }

        [Fact]
        public void CreateOrder_After_Campaign_Created()
        {
            CreateProductCampaignAndOrder();

            var expectedOrderQuantityInPerPeriodOfCampaign = 10;

            Assert.Equal(expectedOrderQuantityInPerPeriodOfCampaign, OrderService.GetOrderQuantityInPerPeriodOfCampaign());
          
        }
        private void CreateProductCampaignAndOrder()
        {
            ProductService.CreateProduct("create_product P11 100 1000");
            CampaignService.CreateCampaign("create_campaign C11 P11 10 20 100");
            OrderService.CreateOrder("create_order P11 10");
        }

    }

}

