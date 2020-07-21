using HBCase.Domain.Models;

namespace HBCase.Domain.Interfaces
{
    public interface ICampaignService
    {
        public void CreateCampaign(string createCampaignCommand);
        public void IncreaseTotalSalesCountAndTurnover(int quantity);
        public void DecreaseCampaignDuration(int hour);
        bool IsCampaignExist(string productCode);
        int GetTargetSalesCountInHour();
        void GetCampaignInfo();
        Campaign GetCampaign();
    }
}
