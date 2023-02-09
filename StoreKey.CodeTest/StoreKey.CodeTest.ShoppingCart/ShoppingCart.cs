using StoreKey.CodeTest.Data.Models;

namespace StoreKey.CodeTest.ShoppingCart
{
    public class ShoppingCart
    {
        private readonly Dictionary<Guid, Item> _items = new();

        public void Add(Product product, int quantity)
        {
            if (_items.TryGetValue(product.Id, out var item))
            {
                quantity += item.Quantity;
            }

            _items[product.Id] = new Item(product, quantity);
        }

        public void Remove(Product product)
        {
            _items.Remove(product.Id);
        }

        public ICollection<Item> Items => _items.Values
            .OrderBy(i => i.Product.DisplayName)
            .ToArray();

        public record Item(Product Product, int Quantity)
        {
            public decimal Total => Product.Price * Quantity;
        }
    }
}