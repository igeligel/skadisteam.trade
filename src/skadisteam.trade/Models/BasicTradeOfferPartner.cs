namespace skadisteam.trade.Models
{
    public class BasicTradeOfferPartner
    {
        public string AvatarUrl { get; set; }

        public string DisplayName { get; set; }

        public int MiniProfileId { get; set; }

        public long SteamCommunityId { get; set; }

        public OnlineStatus OnlineStatus { get; set; }
    }
}
