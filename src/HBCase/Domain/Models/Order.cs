
namespace HBCase.Domain.Models
{
    public class Order
    {
        public Order(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
