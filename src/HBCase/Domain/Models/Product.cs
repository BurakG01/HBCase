using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HBCase.Domain.Models
{
    public class Product
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(string productCode, decimal price,int stock)
        {
            ProductCode = productCode;
            Price = price;
            Stock = stock;
        }
        
    }
}
