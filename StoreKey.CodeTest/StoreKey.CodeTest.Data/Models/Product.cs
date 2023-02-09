namespace StoreKey.CodeTest.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Ean13 { get; set; }
    }
}
