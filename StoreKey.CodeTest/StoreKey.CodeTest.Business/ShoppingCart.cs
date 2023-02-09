namespace StoreKey.CodeTest.Business
{
    public partial class ShoppingCart
    {
        private readonly Dictionary<Guid, ShoppingCartItem> _items;

        public ShoppingCart()
        {
            _items = new();
        }

        public ShoppingCart(params ShoppingCartItem[] items)
        {
            _items = items.ToDictionary(i => i.Product.Id);
        }

        public void Add(Product product, int quantity)
        {
            if (_items.TryGetValue(product.Id, out var item))
            {
                quantity += item.Quantity;
            }

            if (quantity > 0)
            {
                _items[product.Id] = new ShoppingCartItem(product, quantity);
            }
            else
            {
                _items.Remove(product.Id);
            }
        }

        public void Remove(Product product)
        {
            _items.Remove(product.Id);
        }

        public ICollection<ShoppingCartItem> Items => _items.Values;
        public decimal Total => _items.Values.Sum(i => i.Total);
    }
}