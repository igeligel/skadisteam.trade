using System.Collections.Generic;

namespace skadisteam.trade.Models.TradeOffer
{
    public class PlayerTradeStatus
    {
        public List<Asset> Assets { get; set; }

        public bool Ready { get; set; }
    }
}
