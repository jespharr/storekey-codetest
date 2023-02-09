namespace StoreKey.CodeTest.Business.Internal
{
    internal class Edge
    {
        public CampaignNode Campaign { get; private set; }
        public ItemNode Item { get; private set; }

        public Edge(CampaignNode campaign, ItemNode item)
        {
            Campaign = campaign;
            Item = item;
        }

        public void MoveTo(ItemNode item)
        {
            if (item == Item)
            {
                return;
            }

            if (item.Edge != null)
            {
                throw new InvalidOperationException("The specified ItemNode already has an edge");
            }

            Item.Edge = null;
            Item = item;
            item.Edge = this;
        }
    }
}
