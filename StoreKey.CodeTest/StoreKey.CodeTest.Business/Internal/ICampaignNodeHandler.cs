using System.Diagnostics.CodeAnalysis;

namespace StoreKey.CodeTest.Business.Internal
{
    internal interface ICampaignNodeHandler
    {
        IEnumerable<CampaignNode> CreateNodes(Campaign campaign, IEnumerable<ItemNode> items);
        bool TryConnect(CampaignNode node, out ICollection<Conflict> conflicts);
        bool TryFindAlternativeItem(
            Edge edge,
            ICollection<ItemNode> blacklist,
            [NotNullWhen(true)] out ItemNode? alternative);
    }
}
