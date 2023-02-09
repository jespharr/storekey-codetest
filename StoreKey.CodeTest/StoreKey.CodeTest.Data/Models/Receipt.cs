namespace StoreKey.CodeTest.Data.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public List<ProductReceiptItem> ProductItems { get; set; } = null!;
        public List<CampaignReceiptItem> CampaignItems { get; set; } = null!;
    }
}
