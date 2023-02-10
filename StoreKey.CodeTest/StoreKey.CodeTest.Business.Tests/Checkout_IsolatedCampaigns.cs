using FluentAssertions;
using Moq;
using StoreKey.CodeTest.Data;

namespace StoreKey.CodeTest.Business.Tests
{
    public class Checkout_IsolatedCampaigns
    {
        private Checkout _sut;

        [SetUp]
        public void Setup()
        {
            var repositoryMock = new Mock<ICampaignRepository>();
            repositoryMock
                .Setup(x => x.GetApplicableCampaigns(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new[] { TestCampaigns.TwoCokeZero, TestCampaigns.TwoCoCo });

            _sut = new Checkout(repositoryMock.Object);
        }

        [Test]
        public void Applies_no_campaign_when_none_is_applicable()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.CocaCola, 4),
                new ShoppingCartItem(TestProducts.CokeZero, 1));

            var result = _sut.CheckOut(cart);

            result.Total.Should().Be(11.90m * 5);
            result.CampaignItems.Should().BeEmpty();
        }

        [Test]
        public void Applies_campaign_once_when_applicable()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.CocaCola, 2),
                new ShoppingCartItem(TestProducts.CokeZero, 2));

            var result = _sut.CheckOut(cart);

            result.CampaignItems.Should().BeEquivalentTo(new[]
            {
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCokeZero.Id,
                    Quantity = 1,
                    Total = -2m
                }
            }, options => options.Excluding(i => i.DisplayText));
            result.Total.Should().Be(11.90m * 4 - 2m);
        }

        [Test]
        public void Applies_two_campaigns_once_when_applicable()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.CocaCola, 2),
                new ShoppingCartItem(TestProducts.CokeZero, 2),
                new ShoppingCartItem(TestProducts.CoCo, 3));

            var result = _sut.CheckOut(cart);

            result.CampaignItems.Should().BeEquivalentTo(new[]
            {
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCokeZero.Id,
                    Quantity = 1,
                    Total = -2m
                },
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCoCo.Id,
                    Quantity = 1,
                    Total = -7m
                }
            }, options => options.Excluding(i => i.DisplayText));
            result.Total.Should().Be(11.90m * 7 - 2m - 7m);
        }

        [Test]
        public void Applies_two_campaigns_twice_when_applicable()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.CocaCola, 2),
                new ShoppingCartItem(TestProducts.CokeZero, 4),
                new ShoppingCartItem(TestProducts.CoCo, 5));

            var result = _sut.CheckOut(cart);

            result.CampaignItems.Should().BeEquivalentTo(new[]
            {
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCokeZero.Id,
                    Quantity = 2,
                    Total = -4m
                },
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCoCo.Id,
                    Quantity = 2,
                    Total = -14m
                }
            }, options => options.Excluding(i => i.DisplayText));
            result.Total.Should().Be(11.90m * 11 - 4m - 14m);
        }
    }
}
