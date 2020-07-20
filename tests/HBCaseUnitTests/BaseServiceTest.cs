using System;
using System.Collections.Generic;
using System.Text;
using HBCase.Services;

namespace HBCaseUnitTests
{
    public  class BaseServiceTest
    {
        public readonly IOrderService OrderService;
        public readonly IProductService ProductService;
        public readonly ICampaignService CampaignService;

        public BaseServiceTest()
        {
            ProductService = new ProductService();
            CampaignService = new CampaignService(ProductService);
            OrderService = new OrderService(ProductService, CampaignService);
        }
        public void CreateProductCampaignAndOrder()
        {
            ProductService.CreateProduct("create_product P11 100 1000");
            CampaignService.CreateCampaign("create_campaign C11 P11 10 20 100");
            OrderService.CreateOrder("create_order P11 10");
        }
    }
}
