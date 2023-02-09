namespace StoreKey.CodeTest.Business.Internal
{
    internal static class CampaignNodeHandler
    {
        private static readonly ICampaignNodeHandler _comboHandler = new ComboCampaignNodeHandler();
        private static readonly ICampaignNodeHandler _amountHandler = new AmountCampaignNodeHandler();

        public static ICampaignNodeHandler Get(CampaignType type) => type switch
        {
            CampaignType.Combo => _comboHandler,
            CampaignType.Amount => _amountHandler,
            _ => throw new NotSupportedException($"CampaignType {type} is not supported")
        };
    }
}
