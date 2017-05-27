using System;
using System.Collections.Generic;

namespace skadisteam.trade.Models.BasicTradeOffer
{
    public class BasicTradeOffer
    {
        public bool Active { get; set; }

        public DateTime ExpiringDate { get; set; }

        public long Id { get; set; }

        public BasicTradeOfferPartner Partner { get; set; }

        public List<BasicTradeOfferItem> PartnerItems { get; set; }

        public List<BasicTradeOfferItem> ProfileItems { get; set; }
    }
}
