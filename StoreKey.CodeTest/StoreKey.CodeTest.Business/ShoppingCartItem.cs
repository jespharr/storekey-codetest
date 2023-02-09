using StoreKey.CodeTest.Data.Models;

namespace StoreKey.CodeTest.Business
{
    public record ShoppingCartItem(Product Product, int Quantity)
    {
        public decimal Total => Product.Price * Quantity;
    }
}