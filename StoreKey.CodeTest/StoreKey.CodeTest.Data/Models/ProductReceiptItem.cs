namespace StoreKey.CodeTest.Data.Models
{
    public class ProductReceiptItem
    {
        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
