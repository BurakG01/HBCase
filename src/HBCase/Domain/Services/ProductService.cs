using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Models;

namespace HBCase.Domain.Services
{
    public class ProductService : IProductService
    {
        private Product Product;
        private decimal _minPrice;
        private decimal _maxPrice;
        private decimal _decreaseOrIncreasePrice = 5;
        private readonly int _manipulationPercentage = 100;
        public void CreateProduct(string createProductCommand)
        {
            Product = CreateProductByParsing(createProductCommand);

            Console.WriteLine($"Product created ; code {Product.ProductCode}, price {Product.Price} , stock {Product.Stock}");
        }
        public void GetProductInfo(string productCode)
        {
            if (Product.ProductCode == null || Product.ProductCode != productCode)
            {
                throw new Exception("There is no such a product");
            }

            Console.WriteLine($"Product {Product.ProductCode} info ; price {Product.Price} stock {Product.Stock}");
        }
        public void TryToDecreaseProductStock(int quantity)
        {
            if (quantity > Product.Stock)
            {
                throw new Exception($"There is no enough stock");
            }

            Product.Stock -= quantity;
        }
        public Product GetProduct()
        {
            return Product;
        }
        public void SetMinMaxPrice(int campaignManipulationPercentage)
        {
            var manipulationPrice = Product.Price * campaignManipulationPercentage / _manipulationPercentage;

            _minPrice = Product.Price - manipulationPrice;

            _maxPrice = Product.Price + manipulationPrice;
        }
        private Product CreateProductByParsing(string createProductCommand)
        {
            var productProperties = createProductCommand.Split(' ');

            var productCode = productProperties[1];

            if (decimal.TryParse(productProperties[2], out decimal price) &&
                int.TryParse(productProperties[3], out int stock) &&
                !string.IsNullOrEmpty(productCode))
            {
                var product = new Product(productCode, price, stock);

                return product;
            }
            else
            {
                throw new Exception("Invalid Format For Product Properties");
            }
        }
        public void TryToChangeProductPrice(int orderCountInCampaignPeriod, int targetSalesCountInHour)
        {
            if (orderCountInCampaignPeriod == targetSalesCountInHour) return;

            if (orderCountInCampaignPeriod < targetSalesCountInHour)
            {
                var decreasedPrice = Product.Price - _decreaseOrIncreasePrice;

                Product.Price = decreasedPrice >= _minPrice ? decreasedPrice : _minPrice;
            }
            else
            {
                var increasedPrice = Product.Price + _decreaseOrIncreasePrice;

                Product.Price = increasedPrice <= _maxPrice ? increasedPrice : _maxPrice;
            }
        }
    }
}
