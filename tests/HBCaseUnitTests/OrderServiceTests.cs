using System;
using System.Collections.Generic;
using System.Text;
using HBCase.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class OrderServiceTests: BaseServiceTest
    {
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

    }

}

