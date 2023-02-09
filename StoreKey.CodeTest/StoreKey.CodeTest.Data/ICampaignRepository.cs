using StoreKey.CodeTest.Data.Models;

namespace StoreKey.CodeTest.Data
{
    public interface ICampaignRepository
    {
        IEnumerable<Campaign> GetApplicableCampaigns(IEnumerable<Guid> productIds);
    }
}