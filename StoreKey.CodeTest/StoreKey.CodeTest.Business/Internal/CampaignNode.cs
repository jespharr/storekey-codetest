namespace StoreKey.CodeTest.Business.Internal
{
    internal class CampaignNode
    {
        public Campaign Campaign { get; }
        public List<ItemNode> Candidates { get; }
        public int DegreesOfFreedom => Candidates.Count % Campaign.Quantity;
        public bool IsConnected => _edges.Count >= Campaign.Quantity;

        private readonly List<Edge> _edges;

        public CampaignNode(Campaign campaign)
        {
            Campaign = campaign;
            Candidates = new List<ItemNode>();
            _edges = new List<Edge>(campaign.Quantity);
        }

        public void ConnectTo(ItemNode item)
        {
            if (item.Edge != null)
            {
                throw new InvalidOperationException("The specified ItemNode already has an edge");
            }

            if (IsConnected)
            {
                throw new InvalidOperationException("This node already has the maximum number of edges");
            }

            item.Edge = new Edge(this, item);
            _edges.Add(item.Edge);
        }
    }
}
