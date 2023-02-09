namespace StoreKey.CodeTest.Business.Internal
{
    internal record CampaignApplication(Campaign Campaign, int Quantity)
    {
        public decimal Total => Campaign.PriceReductionAmount * Quantity * -1;
    }
}
