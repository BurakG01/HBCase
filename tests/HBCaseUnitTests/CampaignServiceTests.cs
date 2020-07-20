using System;
using System.Collections.Generic;
using System.Text;
using HBCase.Enums;
using HBCase.Services;
using Xunit;

namespace HBCaseUnitTests
{
    public class CampaignServiceTests:BaseServiceTest
    {
        [Fact]
        public void Create_Campaign_With_Invalid_Properties_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => CampaignService.CreateCampaign("create_campaign C1 P4 5 20 sds"));
        }

        [Fact]
        public void Create_Campaign_With_Valid_Properties()
        {
            CreateProductCampaignAndOrder();
            var campaign = CampaignService.GetCampaign();
            Assert.Equal("C11",campaign.Name);
            Assert.Equal("P11",campaign.ProductCode);
            Assert.Equal(10,campaign.Duration);
            Assert.Equal(20,campaign.PriceManipulationLimit);
            Assert.Equal(100,campaign.TargetSalesCount);

        }
        [Fact]
        public void Increase_Campaign_TotalSalesCountAndTurnover_When_Order_Created()
        {
            CreateProductCampaignAndOrder();
            var campaign = CampaignService.GetCampaign();
            Assert.Equal(10,campaign.TotalSalesCount);
            Assert.Equal(1000,campaign.Turnover);
        }

        [Fact]
        public void Campaign_Status_Passive_After_Many_Increase_Time_Command_Arrived()
        {
            CreateProductCampaignAndOrder();

            for (int i = 0; i < 10; i++)
            {
                CampaignService.DecreaseCampaignDuration(1);
            }
            var campaign = CampaignService.GetCampaign();
            Assert.Equal(Status.Passive,campaign.Status);

        }

    }
}
