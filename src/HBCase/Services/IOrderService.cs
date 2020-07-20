using System;
using System.Collections.Generic;
using System.Text;
using HBCase.Domain.Models;

namespace HBCase.Services
{
    public interface IOrderService
    {
        void CreateOrder(string createOrderCommand);
        int GetOrderQuantityInPerPeriodOfCampaign();
        void SetZeroOrderQuantityInPerPeriodOfCampaign();
        public Order GetOrder();
    }
    public class OrderService : IOrderService
    {

        private Order Order;
        private int orderQuantityInPerPeriodOfCampaign;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;

        public OrderService(IProductService productService, ICampaignService campaignService)
        {
            _productService = productService;
            _campaignService = campaignService;
        }

        public void CreateOrder(string createOrderCommand)
        {
            Order = CreateOrderByParsing(createOrderCommand);

            _productService.TryToDecreaseProductStock(Order.Quantity);

            if (_campaignService.IsCampaignExist(Order.ProductCode))
            {
                orderQuantityInPerPeriodOfCampaign += Order.Quantity;

                _campaignService.IncreaseTotalSalesCountAndTurnover(Order.Quantity);
            }

            Console.WriteLine($"Order created; product {Order.ProductCode}, quantity {Order.Quantity}");

        }
        public int GetOrderQuantityInPerPeriodOfCampaign()
        {
            return orderQuantityInPerPeriodOfCampaign;
        }

        public void SetZeroOrderQuantityInPerPeriodOfCampaign()
        {
            orderQuantityInPerPeriodOfCampaign = 0;
        }

        public Order GetOrder()
        {
            return Order;
        }

        private Order CreateOrderByParsing(string createOrderCommand)
        {
            var orderProperties = createOrderCommand.Split(' ');

            var productCode = orderProperties[1];
            if (int.TryParse(orderProperties[2], out int quantity) && !string.IsNullOrEmpty(productCode))
            {
                var order = new Order(productCode, quantity);
                return order;
            }
            else
            {
                throw new Exception("Invalid Format For Order Properties");
            }
        }
    }
}
