namespace skadisteam.trade.Models.TradeOffer
{
    public class SkadiTradeOffer
    {
        public int Id { get; set; }
        public SkadiTradeOfferProfile MyProfile { get; set; }
        
        public SkadiTradeOfferPartner Partner { get; set; }

        public SkadiEscrowData SkadiEscrowData { get; set; }

        public SkadiAppDataContext SkadiAppDataContext { get; set; }

        public string TradeOfferNote { get; set; }

        public TradeStatus TradeStatus { get; set; }
        
        public string SessionId { get; set; }
    }
}
