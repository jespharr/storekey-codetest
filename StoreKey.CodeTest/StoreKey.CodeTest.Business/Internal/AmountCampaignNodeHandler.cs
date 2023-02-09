using System.Diagnostics.CodeAnalysis;

namespace StoreKey.CodeTest.Business.Internal
{
    internal class AmountCampaignNodeHandler : ICampaignNodeHandler
    {
        public IEnumerable<CampaignNode> CreateNodes(Campaign campaign, IEnumerable<ItemNode> items)
        {
            var groups = items
                .GroupBy(i => i.Product)
                .Where(g => campaign.ProductIds.Contains(g.Key.Id))
                .Select(g => g.ToArray())
                .Where(g => g.Length >= campaign.Quantity);

            foreach (var candidates in groups)
            {
                var nodeCount = candidates.Length / campaign.Quantity;
                for (var i = 0; i < nodeCount; i++)
                {
                    var node = new CampaignNode(campaign);
                    node.Candidates.AddRange(candidates);
                    yield return node;
                }
            }
        }

        public bool TryConnect(CampaignNode node, out ICollection<Conflict> conflicts)
        {
            conflicts = new List<Conflict>();
            var eligible = node.Candidates.Where(i => i.Edge == null).ToArray();
            if (eligible.Length < node.Campaign.Quantity)
            {
                var potentiallyResolvable = node.Candidates
                    .Select(i => i.Edge)
                    .OfType<Edge>()
                    .Where(e => e.CampaignNode.Campaign != node.Campaign)
                    .ToArray();

                var deficit = node.Campaign.Quantity - eligible.Length;
                if (potentiallyResolvable.Length >= deficit)
                {
                    conflicts.Add(new Conflict(deficit, potentiallyResolvable, node.Candidates));
                }
            }
            else
            {
                foreach (var candidate in eligible.Take(node.Campaign.Quantity))
                {
                    node.ConnectTo(candidate);
                }

                return true;
            }

            return false;
        }

        public bool TryFindAlternativeItem(
            Edge edge,
            ICollection<ItemNode> blacklist,
            [NotNullWhen(true)] out ItemNode? alternative)
        {
            alternative = null;
            return false;
        }
    }
}
