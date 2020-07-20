using System;
using System.Collections.Generic;
using System.Text;
using HBCase.Enums;

namespace HBCase.Domain.Models
{
    public class Campaign
    {
        public Campaign(string name,string productCode,int priceManipulationLimit,int duration,int targetSalesCount,Status status)
        {
            Name = name;
            ProductCode = productCode;
            PriceManipulationLimit = priceManipulationLimit;
            Duration = duration;
            TargetSalesCount = targetSalesCount;
            Status = status;
        }
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public int Duration { get; set; }

        public int PriceManipulationLimit { get; set; }

        public int TargetSalesCount { get; set; }

        public Status Status { get; set; }

        public int TotalSalesCount { get; set; }

        public decimal Turnover { get; set; }

        public decimal AverageItemPrice { get; set; }
    }
}
