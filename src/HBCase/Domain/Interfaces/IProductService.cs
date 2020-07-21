using HBCase.Domain.Models;

namespace HBCase.Domain.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(string createProductCommand);
        void GetProductInfo(string productCode);
        void TryToChangeProductPrice(int orderCountInCampaignPeriod, int targetSalesCountInHour);
        void TryToDecreaseProductStock(int quantity);
        Product GetProduct();
        void SetMinMaxPrice(int campaignManipulationLimit);
    }
}
