namespace StoreKey.CodeTest.Business.Internal
{
    internal class CampaignGraph
    {
        public List<CampaignNode> Nodes { get; } = new List<CampaignNode>();

        public CampaignGraph(ICollection<ShoppingCartItem> items, IEnumerable<Campaign> campaigns)
        {
            var itemNodes = items
                .SelectMany(i => Enumerable
                    .Range(0, i.Quantity)
                    .Select(_ => new ItemNode(i.Product)))
                .ToList();

            foreach (var campaign in campaigns)
            {
                var handler = CampaignNodeHandler.Get(campaign.Type);
                Nodes.AddRange(handler.CreateNodes(campaign, itemNodes));
            }
        }
    }
}
