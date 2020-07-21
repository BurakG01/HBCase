using HBCase.Domain.Models;

namespace HBCase.Domain.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(string createOrderCommand);
        int GetOrderQuantityInPerPeriodOfCampaign();
        void SetZeroOrderQuantityInPerPeriodOfCampaign();
        public Order GetOrder();
    }
}
