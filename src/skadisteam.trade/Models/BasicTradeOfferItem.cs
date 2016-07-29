namespace skadisteam.trade.Models
{
    public class BasicTradeOfferItem
    {
        public int AppId { get; set; }

        public int AssetId { get; set; }

        public int ContextId { get; set; }

        public long SteamCommunityId { get; set; }
        
        public string ItemPicture { get; set; }

        public bool Missing { get; set; }
    }
}
