namespace StoreKey.CodeTest.Business.Tests
{
    public class ShoppingCart_NotEmpty
    {
        private ShoppingCart _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ShoppingCart(
                new ShoppingCartItem(TestProducts.CocaCola, 5),
                new ShoppingCartItem(TestProducts.CocaColaLight, 2),
                new ShoppingCartItem(TestProducts.CokeZero, 6));
        }

        [Test]
        public void Should_add_to_existing_item_when_adding_positive_quantity()
        {
            _sut.Add(TestProducts.CocaCola, 2);
            Assert.That(_sut.Items, Has.Count.EqualTo(3), nameof(_sut.Items));
            Assert.That(_sut.Items, Contains.Item(new ShoppingCartItem(TestProducts.CocaCola, 7)), nameof(_sut.Items));
        }

        [Test]
        public void Should_do_nothing_when_adding_zero_quantity()
        {
            _sut.Add(TestProducts.CocaCola, 0);
            Assert.That(_sut.Items, Has.Count.EqualTo(3), nameof(_sut.Items));
            Assert.That(_sut.Items, Contains.Item(new ShoppingCartItem(TestProducts.CocaCola, 5)), nameof(_sut.Items));
        }

        [Test]
        public void Should_subtract_from_existing_item_when_adding_negative_quantity()
        {
            _sut.Add(TestProducts.CocaCola, -2);
            Assert.That(_sut.Items, Has.Count.EqualTo(3), nameof(_sut.Items));
            Assert.That(_sut.Items, Contains.Item(new ShoppingCartItem(TestProducts.CocaCola, 3)), nameof(_sut.Items));
        }

        [Test]
        public void Should_remove_item_when_adding_negative_quantity_equal_to_existing_quantity()
        {
            _sut.Add(TestProducts.CocaCola, -5);
            Assert.That(_sut.Items, Has.Count.EqualTo(2), nameof(_sut.Items));
            Assert.That(_sut.Items, Has.None.Matches<ShoppingCartItem>(i => i.Product == TestProducts.CocaCola), nameof(_sut.Items));
        }

        [Test]
        public void Should_remove_item_when_adding_negative_quantity_exceeding_existing_quantity()
        {
            _sut.Add(TestProducts.CocaCola, -6);
            Assert.That(_sut.Items, Has.Count.EqualTo(2), nameof(_sut.Items));
            Assert.That(_sut.Items, Has.None.Matches<ShoppingCartItem>(i => i.Product == TestProducts.CocaCola), nameof(_sut.Items));
        }

        [Test]
        public void Should_remove_item_when_removing_existing_product()
        {
            _sut.Remove(TestProducts.CocaCola);
            Assert.That(_sut.Items, Has.Count.EqualTo(2), nameof(_sut.Items));
            Assert.That(_sut.Items, Has.None.Matches<ShoppingCartItem>(i => i.Product == TestProducts.CocaCola), nameof(_sut.Items));
        }

        [Test]
        public void Should_have_items()
        {
            Assert.That(_sut.Items, Has.Count.EqualTo(3), nameof(_sut.Items));
        }

        [Test]
        public void Should_have_non_zero_total()
        {
            Assert.That(_sut.Total, Is.EqualTo(154.70m), nameof(_sut.Total));
        }
    }
}