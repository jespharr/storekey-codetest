namespace StoreKey.CodeTest.Business.Internal
{
    internal class ItemNode
    {
        public Product Product { get; }
        public Edge? Edge { get; set; }

        public ItemNode(Product product)
        {
            Product = product;
        }

        public override string ToString() => Edge == null
            ? $"{Product} (Not Connected)"
            : $"{Product} (Connected)";
    }
}
