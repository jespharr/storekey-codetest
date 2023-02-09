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
                var candidates = itemNodes
                    .Where(i => campaign.ProductIds.Contains(i.Product.Id))
                    .ToArray();

                var nodeCount = candidates.Length / campaign.Quantity;
                for (var i = 0; i < nodeCount; i++)
                {
                    var node = new CampaignNode(campaign);
                    foreach (var candidate in candidates)
                    {
                        node.Candidates.Add(candidate);
                    }
                    Nodes.Add(node);
                }
            }
        }
    }
}
