using StoreKey.CodeTest.Business.Internal;
using StoreKey.CodeTest.Data;
using StoreKey.CodeTest.Data.Models;

namespace StoreKey.CodeTest.Business
{
    public class Checkout
    {
        private readonly ICampaignRepository _campaignRepository;

        public Checkout(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public Receipt CheckOut(ShoppingCart cart)
        {
            var campaigns = _campaignRepository.GetApplicableCampaigns(cart.Items.Select(i => i.Product.Id));
            var applications = CampaignCalculations.FindHighestValueCampaignApplications(cart, campaigns);
            var campaignItems = applications.Select(ToReceiptItem).ToList();
            var productItems = cart.Items.Select(ToReceiptItem).ToList();

            return new Receipt
            {
                Total = productItems.Sum(i => i.Total) + campaignItems.Sum(i => i.Total),
                Timestamp = DateTimeOffset.Now,
                ProductItems = productItems,
                CampaignItems = campaignItems
            };
        }

        private static ProductReceiptItem ToReceiptItem(ShoppingCartItem item) => new()
        {
            ProductId = item.Product.Id,
            Quantity = item.Quantity,
            Total = item.Total
        };

        private static CampaignReceiptItem ToReceiptItem(CampaignApplication application) => new()
        {
            CampaignId = application.Campaign.Id,
            Quantity = application.Quantity,
            Total = application.Total
        };
    }
}
