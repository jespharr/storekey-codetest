using System.Diagnostics.CodeAnalysis;

namespace StoreKey.CodeTest.Business.Internal
{
    internal class ComboCampaignNodeHandler : ICampaignNodeHandler
    {
        public IEnumerable<CampaignNode> CreateNodes(Campaign campaign, IEnumerable<ItemNode> items)
        {
            var candidates = items
                .Where(i => campaign.ProductIds.Contains(i.Product.Id))
                .ToArray();

            var nodeCount = candidates.Length / campaign.Quantity;
            for (var i = 0; i < nodeCount; i++)
            {
                var node = new CampaignNode(campaign);
                node.Candidates.AddRange(candidates);
                yield return node;
            }
        }

        public bool TryConnect(CampaignNode node, out ICollection<Conflict> conflicts)
        {
            var eligible = node.Candidates.Where(c => c.Edge == null).ToArray();
            if (eligible.Length < node.Campaign.Quantity)
            {
                var deficit = node.Campaign.Quantity - eligible.Length;
                var potentiallyResolvable = node.Candidates
                    .Select(i => i.Edge)
                    .OfType<Edge>()
                    .Where(e => e.CampaignNode.Campaign != node.Campaign)
                    .ToArray();
                
                if (potentiallyResolvable.Length >= deficit)
                {
                    conflicts = new[]
                    {
                        new Conflict(deficit, potentiallyResolvable, node.Candidates)
                    };
                }
                else
                {
                    conflicts = Array.Empty<Conflict>();
                }

                return false;
            }
            else
            {
                foreach (var candidate in eligible.Take(node.Campaign.Quantity))
                {
                    node.ConnectTo(candidate);
                }

                conflicts = Array.Empty<Conflict>();
                return true;
            }
        }

        public bool TryFindAlternativeItem(
            Edge edge,
            ICollection<ItemNode> blacklist,
            [NotNullWhen(true)] out ItemNode? alternative)
        {
            alternative = edge.CampaignNode.Candidates
                .Where(i => i.Edge == null)
                .Where(i => !blacklist.Contains(i))
                .FirstOrDefault();

            return alternative != null;
        }
    }
}
