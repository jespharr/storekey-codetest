namespace StoreKey.CodeTest.Business.Tests
{
    public static class TestCampaigns
    {
        public static readonly Campaign TwoCokeZero = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "2 SEK off when buying two Coke Zero",
            Type = CampaignType.Amount,
            PriceReductionAmount = 2,
            Quantity = 2,
            ProductIds = new List<Guid>
            {
                TestProducts.CokeZero.Id
            }
        };

        public static readonly Campaign FourCandyBars = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Four candy bars",
            Type = CampaignType.Combo,
            PriceReductionAmount = 12,
            Quantity = 4,
            ProductIds = new List<Guid>
            {
                TestProducts.Kexchoklad.Id,
                TestProducts.CoCo.Id,
                TestProducts.Japp.Id
            }
        };

        public static readonly Campaign TwoMarabouProducts = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Two Marabou products",
            Type = CampaignType.Combo,
            PriceReductionAmount = 5,
            Quantity = 2,
            ProductIds = new List<Guid>
            {
                TestProducts.CoCo.Id,
                TestProducts.Japp.Id
            }
        };

        public static readonly Campaign TwoCoCo = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Two Co-Co",
            Type = CampaignType.Amount,
            PriceReductionAmount = 6,
            Quantity = 2,
            ProductIds = new List<Guid>
            {
                TestProducts.CoCo.Id
            }
        };

        public static readonly Campaign ThreeForTwoKexchoklad = new()
        {
            Id = Guid.NewGuid(),
            DisplayName = "Three Kexchoklad for two",
            Type = CampaignType.Amount,
            PriceReductionAmount = 8.95m,
            Quantity = 3,
            ProductIds = new List<Guid>
            {
                TestProducts.Kexchoklad.Id
            }
        };
    }
}
