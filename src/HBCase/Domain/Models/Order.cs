using System;
using System.Collections.Generic;
using System.Text;

namespace HBCase.Domain.Models
{
    public class Order
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

        public Order(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }
    }
}
