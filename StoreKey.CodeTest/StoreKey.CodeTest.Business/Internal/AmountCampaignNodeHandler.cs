using System.Diagnostics.CodeAnalysis;

namespace StoreKey.CodeTest.Business.Internal
{
    internal class AmountCampaignNodeHandler : ICampaignNodeHandler
    {
        public bool TryConnect(CampaignNode node, out ICollection<Conflict> conflicts)
        {
            conflicts = new List<Conflict>();
            var productGroups = node.Candidates.GroupBy(i => i.Product);

            foreach (var group in productGroups)
            {
                var eligible = group.Where(i => i.Edge == null).ToArray();
                if (eligible.Length < node.Campaign.Quantity)
                {
                    conflicts.Add(new Conflict(
                        Deficit: node.Campaign.Quantity - eligible.Length,
                        Edges: group.Select(i => i.Edge).OfType<Edge>().ToArray(),
                        Blacklist: group.ToArray()));
                }
                else
                {
                    foreach (var candidate in eligible
                        .Where(i => i.Edge == null)
                        .Take(node.Campaign.Quantity))
                    {
                        node.ConnectTo(candidate);
                    }

                    return true;
                }
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
