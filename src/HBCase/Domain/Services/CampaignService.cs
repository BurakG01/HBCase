using System;
using HBCase.Domain.Interfaces;
using HBCase.Domain.Models;
using HBCase.Enums;

namespace HBCase.Domain.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IProductService _productService;
        private int _targetSalesCountInHour;
        private Campaign Campaign;
        public CampaignService(IProductService productService)
        {
            _productService = productService;
        }
        public void CreateCampaign(string createCampaignCommand)
        {
            Campaign = CreateCampaignByParsing(createCampaignCommand);

            Console.WriteLine($"Campaign created; name {Campaign.Name}, product {Campaign.ProductCode}, duration {Campaign.Duration}, limit {Campaign.PriceManipulationLimit}, target sales count {Campaign.TargetSalesCount}");

            SetTargetSalesCountInHour(Campaign.TargetSalesCount, Campaign.Duration);
           
            _productService.SetMinMaxPrice(Campaign.PriceManipulationLimit); 
        }
        private Campaign CreateCampaignByParsing(string createProductCommand)
        {
            var campaignProperties = createProductCommand.Split(' ');

            var name = campaignProperties[1];

            var productCode = campaignProperties[2];
            if (int.TryParse(campaignProperties[3], out int duration) &&
                int.TryParse(campaignProperties[4], out int priceManipulationLimit) &&
                int.TryParse(campaignProperties[5], out int targetSalesCount) &&
                !string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(productCode))
            {
                var campaign = new Campaign(name, productCode, priceManipulationLimit, duration, targetSalesCount, Status.Active);

                return campaign;
            }
            else
            {
                throw new Exception("Invalid Format For Campaign Properties");
            }
        }
        public int GetTargetSalesCountInHour()
        {
            return _targetSalesCountInHour;
        }
        private void SetTargetSalesCountInHour(int campaignTargetSalesCount, int campaignDuration)
        {
            _targetSalesCountInHour = campaignTargetSalesCount / campaignDuration;
        }
        public bool IsCampaignExist(string productCode)
        {
            return (Campaign != null) && (Campaign.ProductCode == productCode) && (Campaign.Status == Status.Active);
        }
        public void IncreaseTotalSalesCountAndTurnover(int quantity) 
        {
            var currentPriceOfProduct = _productService.GetProduct().Price;

            Campaign.TotalSalesCount += quantity;

            Campaign.Turnover = Campaign.Turnover + (currentPriceOfProduct * quantity);

            Campaign.Turnover = Decimal.Round(Campaign.Turnover, 3);
        }
        public void SetAverageItemPrice()
        {
            Campaign.AverageItemPrice = Campaign.Turnover / Campaign.TotalSalesCount;

            Campaign.AverageItemPrice = Decimal.Round(Campaign.AverageItemPrice, 3);
        }
        public void DecreaseCampaignDuration(int hour)
        {
            Campaign.Duration -= hour;

            if (Campaign.Duration <= 0)
            {
                Campaign.Status = Status.Passive;
            }
        }
        public Campaign GetCampaign()
        {
            return Campaign;
        }
        public void GetCampaignInfo()
        {
            Console.WriteLine($"Campaign {Campaign.Name} info;  Status {Campaign.Status},  Target Sales {Campaign.TargetSalesCount},  Total Sales {Campaign.TotalSalesCount},  Turnover {Campaign.Turnover},  Average Item Price {Campaign.AverageItemPrice}");
        }
    }
}
