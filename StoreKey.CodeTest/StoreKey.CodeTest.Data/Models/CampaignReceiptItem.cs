namespace StoreKey.CodeTest.Data.Models
{
    public class CampaignReceiptItem
    {
        public Guid ReceiptId { get; set; }
        public Guid CampaignId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string DisplayText { get; set; } = null!;

        public override string ToString() => DisplayText;
    }
}
