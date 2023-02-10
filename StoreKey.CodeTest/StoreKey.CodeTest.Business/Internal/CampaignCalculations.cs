namespace StoreKey.CodeTest.Business.Internal
{
    internal static class CampaignCalculations
    {
        public static IEnumerable<CampaignApplication> FindHighestValueCampaignApplications(ShoppingCart cart, IEnumerable<Campaign> campaigns)
        {
            var graph = new CampaignGraph(cart.Items, campaigns);
            var nodes = graph.Nodes
                .OrderByDescending(x => x.Campaign.PriceReductionAmount) // Highest value campaigns first to maximize odds of finding optimal solution
                .ThenBy(x => x.Campaign.Quantity) // Followed by least number of items consumed (better total potential)
                .ThenBy(x => x.DegreesOfFreedom); // Lastly, by lowest degree of freedom to minimize number of conflicts to resolve

            bool conflictResolved;
            do
            {
                conflictResolved = false;
                foreach (var node in nodes.Where(x => !x.IsConnected))
                {
                    var handler = CampaignNodeHandler.Get(node.Campaign.Type);
                    if (!handler.TryConnect(node, out var conflicts))
                    {
                        foreach (var conflict in conflicts)
                        {
                            if (TryResolveConflict(node.Campaign, conflict))
                            {
                                conflictResolved = true;
                                handler.TryConnect(node, out _);
                                break;
                            }
                        }
                    }
                }
            }
            while (conflictResolved);

            return graph.Nodes
                .GroupBy(n => n.Campaign)
                .Select(g => new CampaignApplication(g.Key, g.Count(n => n.IsConnected)))
                .Where(a => a.Quantity > 0);
        }

        private static bool TryResolveConflict(Campaign campaign, Conflict conflict)
        {
            // See if edges can be moved around to "make room"
            var moved = 0;
            foreach (var edge in conflict.Edges)
            {
                var handler = CampaignNodeHandler.Get(edge.CampaignNode.Campaign.Type);
                if (handler.TryFindAlternativeItem(edge, conflict.Blacklist, out var alternative))
                {
                    edge.MoveTo(alternative);
                    if (++moved == conflict.Deficit)
                    {
                        return true;
                    }
                }
            }

            // See if we can replace an applied campaign with lower price reduction
            var bestCandidate = conflict.Edges
                .ToLookup(e => e.CampaignNode)
                .Where(g => g.Count() >= conflict.Deficit)
                .Select(g => g.Key)
                .OrderBy(n => n.Campaign.PriceReductionAmount)
                .FirstOrDefault();

            if (bestCandidate != null && bestCandidate.Campaign.PriceReductionAmount < campaign.PriceReductionAmount)
            {
                bestCandidate.Disconnect();
                return true;
            }

            return false;
        }
    }
}
