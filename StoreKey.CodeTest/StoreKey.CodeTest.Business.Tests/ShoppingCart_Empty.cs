namespace StoreKey.CodeTest.Business.Tests
{
    public class ShoppingCart_Empty
    {
        private ShoppingCart _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ShoppingCart();
        }

        [Test]
        public void Should_add_new_item_when_adding_positive_quantity()
        {
            _sut.Add(TestProducts.CocaCola, 5);
            Assert.That(_sut.Items, Has.Count.EqualTo(1), nameof(_sut.Items));
            Assert.That(_sut.Items, Contains.Item(new ShoppingCartItem(TestProducts.CocaCola, 5)), nameof(_sut.Items));
        }

        [Test]
        public void Should_do_nothing_when_adding_zero_quantity()
        {
            _sut.Add(TestProducts.CocaCola, 0);
            Assert.That(_sut.Items, Has.Count.EqualTo(0), nameof(_sut.Items));
        }

        [Test]
        public void Should_do_nothing_when_adding_negative_quantity()
        {
            _sut.Add(TestProducts.CocaCola, -1);
            Assert.That(_sut.Items, Has.Count.EqualTo(0), nameof(_sut.Items));
        }

        [Test]
        public void Should_not_throw_when_removing()
        {
            Assert.DoesNotThrow(() => _sut.Remove(TestProducts.CocaCola));
        }

        [Test]
        public void Should_have_no_items()
        {
            Assert.That(_sut.Items, Has.Count.EqualTo(0), nameof(_sut.Items));
        }

        [Test]
        public void Should_have_zero_total()
        {
            Assert.That(_sut.Total, Is.EqualTo(0m), nameof(_sut.Total));
        }
    }
}