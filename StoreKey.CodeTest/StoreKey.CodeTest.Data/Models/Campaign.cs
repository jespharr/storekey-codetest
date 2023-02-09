namespace StoreKey.CodeTest.Data.Models
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public CampaignType Type { get; set; }
        public decimal PriceReductionAmount { get; set; }
        public int Quantity { get; set; }
        public List<Guid> ProductIds { get; set; } = new List<Guid>();

        public decimal Value => PriceReductionAmount / Quantity;

        public override string ToString() => $"{DisplayName}: -{PriceReductionAmount}";
    }
}
