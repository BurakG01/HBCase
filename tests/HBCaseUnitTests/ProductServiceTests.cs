using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class ProductServiceTests 
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService();
            ICampaignService campaignService = new CampaignService(_productService);
            _orderService = new OrderService(_productService, campaignService);
        }

        [Fact]
        public void CreateProduct_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => _productService.CreateProduct("create_product P11 asd 1000"));
        }
        [Fact]
        public void Product_Should_Be_Created()
        {
            _productService.CreateProduct("create_product P11 100 1000");
            var product = _productService.GetProduct();
            Assert.Equal("P11",product.ProductCode);
            Assert.Equal(100,product.Price);
            Assert.Equal(1000,product.Stock);

        }
        [Fact]
        public void Product_Stock_Should_Decrease_When_Order_Created()
        {
            _productService.CreateProduct("create_product P11 100 1000");
            _orderService.CreateOrder("create_order P11 10");
            int expectedProductStock = 990;
            var product = _productService.GetProduct();
            Assert.Equal(expectedProductStock, product.Stock);
        }
        [Fact]
        public void Product_Price_Change_When_Time_Increase_In_Campaign_Period()
        {
            _productService.CreateProduct("create_product P11 100 1000");
            _productService.TryToChangeProductPrice(10,15);
            var expectedProductPrice = 95;
            var product = _productService.GetProduct();
            Assert.Equal(expectedProductPrice,product.Price);
        }
        [Fact]
        public void Decrease_ProductStock_Should_Throw_Exception_When_Quantity_Greater_Then_Stock()
        {
            _productService.CreateProduct("create_product P11 100 40");

            Assert.Throws<Exception>(() => _productService.TryToDecreaseProductStock(50));
        }

    }
    
  
}
