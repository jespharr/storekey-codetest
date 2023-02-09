namespace StoreKey.CodeTest.Business.Internal
{
    internal static class CampaignCalculations
    {
        public static IEnumerable<CampaignApplication> FindHighestValueCampaignApplications(ShoppingCart cart, IEnumerable<Campaign> campaigns)
        {
            var graph = new CampaignGraph(cart.Items, campaigns);
            var nodes = graph.Nodes
                .OrderByDescending(x => x.Campaign.Value) // Highest value campaigns first to maximize odds of finding optimal solution
                .ThenBy(x => x.DegreesOfFreedom) // Followed by lowest degree of freedom to minimize movement of edges when trying to resolve conflicts (not sure if this is really useful...)
                .ThenBy(x => x.Campaign.Quantity); // Lastly, by least number of items consumed to minimize the number of conflicts (probably?)

            foreach (var node in nodes)
            {
                var handler = GetHandler(node);
                if (!handler.TryConnect(node, out var conflicts))
                {
                    foreach (var conflict in conflicts)
                    {
                        if (TryResolveConflict(conflict))
                        {
                            handler.TryConnect(node, out _);
                            break;
                        }
                    }
                }
            }

            return nodes
                .GroupBy(n => n.Campaign)
                .Select(g => new CampaignApplication(g.Key, g.Count(n => n.IsConnected)));
        }

        private static bool TryResolveConflict(Conflict conflict)
        {
            var moved = 0;
            foreach (var edge in conflict.Edges)
            {
                var handler = GetHandler(edge.Campaign);
                if (handler.TryFindAlternativeItem(edge, conflict.Blacklist, out var alternative))
                {
                    edge.MoveTo(alternative);
                    if (++moved == conflict.Deficit)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static readonly ICampaignNodeHandler _comboHandler = new ComboCampaignNodeHandler();
        private static readonly ICampaignNodeHandler _amountHandler = new AmountCampaignNodeHandler();
        private static ICampaignNodeHandler GetHandler(CampaignNode node) => node.Campaign.Type switch
        {
            CampaignType.Combo => _comboHandler,
            CampaignType.Amount => _amountHandler,
            _ => throw new NotSupportedException($"CampaignType {node.Campaign.Type} is not supported")
        };
    }
}
