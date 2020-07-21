namespace HBCase.Domain.Models
{
    public class Product
    {
        public Product(string productCode, decimal price, int stock)
        {
            ProductCode = productCode;
            Price = price;
            Stock = stock;
        }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
