using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class ProductServiceTests 
    {
        public readonly IOrderService OrderService;
        public readonly IProductService ProductService;
        public readonly ICampaignService CampaignService;

        public ProductServiceTests()
        {
            ProductService = new ProductService();
            CampaignService = new CampaignService(ProductService);
            OrderService = new OrderService(ProductService, CampaignService);
        }

        [Fact]
        public void CreateProduct_With_Invalid_Properties_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => ProductService.CreateProduct("create_product P11 asd 1000"));
        }

        [Fact]
        public void CreateProduct_With_Valid_Properties()
        {
            ProductService.CreateProduct("create_product P11 100 1000");
            var product = ProductService.GetProduct();
            Assert.Equal("P11",product.ProductCode);
            Assert.Equal(100,product.Price);
            Assert.Equal(1000,product.Stock);

        }
        [Fact]
        public void Product_Stock_Decrease_When_Order_Created()
        {
            ProductService.CreateProduct("create_product P11 100 1000");
            OrderService.CreateOrder("create_order P11 10");
            int expectedProductStock = 990;
            var product = ProductService.GetProduct();
            Assert.Equal(expectedProductStock, product.Stock);
        }

        [Fact]
        public void Product_Price_Change_When_Time_Increase_In_Campaign_Period()
        {
            ProductService.CreateProduct("create_product P11 100 1000");
            ProductService.TryToChangeProductPrice(10,15);
            var expectedProductPrice = 95;
            var product = ProductService.GetProduct();
            Assert.Equal(expectedProductPrice,product.Price);
        }

        [Fact]
        public void TryToProductStock_Decrease_Should_Throw_Exception_When_Quantity_Greater_Then_Stock()
        {
            ProductService.CreateProduct("create_product P11 100 40");

            Assert.Throws<Exception>(() => ProductService.TryToDecreaseProductStock(50));
        }

    }
    
  
}
