using System;

namespace skadisteam.trade.Models.TradeOffer
{
    public class SkadiTradeOfferPartner
    {
        public string PartnerDisplayName { get; set; }
        public long PartnerCommunityId { get; set; }
        public long PartnerMiniProfileId { get; set; }
        public int PartnerLevel { get; set; }
        public DateTime PartnerIsMemberSince { get; set; }
        public DateTime FriendsSince { get; set; }
        public OnlineStatus PartnerOnlineStatus { get; set; }
        public bool PartnerOnProbation { get; set; }
    }
}
