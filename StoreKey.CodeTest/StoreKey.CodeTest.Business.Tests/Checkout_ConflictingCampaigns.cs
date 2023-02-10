using FluentAssertions;
using Moq;
using StoreKey.CodeTest.Data;

namespace StoreKey.CodeTest.Business.Tests
{
    public class Checkout_ConflictingCampaigns
    {
        private Checkout _sut;

        [SetUp]
        public void Setup()
        {
            var repositoryMock = new Mock<ICampaignRepository>();
            repositoryMock
                .Setup(x => x.GetApplicableCampaigns(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new[]
                {
                    TestCampaigns.ThreeForTwoKexchoklad,
                    TestCampaigns.TwoCoCo,
                    TestCampaigns.TwoMarabouProducts,
                    TestCampaigns.FourCandyBars,
                    TestCampaigns.ThreeForTwoJapp
                });

            _sut = new Checkout(repositoryMock.Object);
        }

        [Test]
        public void Guarantee_lowest_total_price_1()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.Kexchoklad, 3), // Attempting to trick FourCandyBars campaign into select Kexchoklad candidates by putting them first in the cart
                new ShoppingCartItem(TestProducts.CoCo, 3),
                new ShoppingCartItem(TestProducts.Japp, 1));

            var result = _sut.CheckOut(cart);

            result.CampaignItems.Should().BeEquivalentTo(new[]
            {
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.FourCandyBars.Id,
                    Quantity = 1,
                    Total = -12m
                },
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.ThreeForTwoKexchoklad.Id,
                    Quantity = 1,
                    Total = -8.95m
                }
            }, options => options.Excluding(i => i.DisplayText));
            result.Total.Should().Be(8.95m * 3 + 11.90m * 3 + 10.95m * 1 - 12m - 8.95m);
        }

        [Test]
        public void Guarantee_lowest_total_price_2()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.Kexchoklad, 1),
                new ShoppingCartItem(TestProducts.CoCo, 6));

            var result = _sut.CheckOut(cart);

            result.CampaignItems.Should().BeEquivalentTo(new[]
            {
                new CampaignReceiptItem
                {
                    CampaignId = TestCampaigns.TwoCoCo.Id,
                    Quantity = 3,
                    Total = -21m
                }
            }, options => options.Excluding(i => i.DisplayText));
            result.Total.Should().Be(8.95m * 1 + 10.95m * 6 - 21.90m);
        }
    }
}
