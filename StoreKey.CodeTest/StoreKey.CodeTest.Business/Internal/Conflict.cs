namespace StoreKey.CodeTest.Business.Internal
{
    internal record Conflict(
        int Deficit,
        ICollection<Edge> Edges,
        ICollection<ItemNode> Blacklist);
}
