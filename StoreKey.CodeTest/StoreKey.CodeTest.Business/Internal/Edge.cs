namespace StoreKey.CodeTest.Business.Internal
{
    internal class Edge
    {
        public CampaignNode CampaignNode { get; private set; }
        public ItemNode ItemNode { get; private set; }

        public Edge(CampaignNode campaign, ItemNode item)
        {
            CampaignNode = campaign;
            ItemNode = item;
        }

        public void MoveTo(ItemNode item)
        {
            if (item == ItemNode)
            {
                return;
            }

            if (item.Edge != null)
            {
                throw new InvalidOperationException("The specified ItemNode already has an edge");
            }

            ItemNode.Edge = null;
            ItemNode = item;
            item.Edge = this;
        }

        public override string ToString() => $"{CampaignNode.Campaign} -> {ItemNode.Product}";
    }
}
