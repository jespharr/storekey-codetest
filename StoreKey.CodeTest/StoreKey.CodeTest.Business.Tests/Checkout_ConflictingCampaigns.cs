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
                    TestCampaigns.FourCandyBars
                });

            _sut = new Checkout(repositoryMock.Object);
        }

        [Test]
        public void Resolves_conflicts_prioritizing_highest_value()
        {
            var cart = new ShoppingCart(
                new ShoppingCartItem(TestProducts.Kexchoklad, 3),
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
    }
}
