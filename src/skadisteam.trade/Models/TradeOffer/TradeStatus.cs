namespace skadisteam.trade.Models.TradeOffer
{
    public class TradeStatus
    {
        public bool NewVersion { get; set; }

        public int Version { get; set; }

        public PlayerTradeStatus Me { get; set; }

        public PlayerTradeStatus Them { get; set; }
    }
}
