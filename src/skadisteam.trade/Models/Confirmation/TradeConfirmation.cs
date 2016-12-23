using System.Collections.Generic;

namespace skadisteam.trade.Models.Confirmation
{
    public class TradeConfirmation : MobileConfirmation
    {
        public OnlineStatus PartnerOnlineStatus { get; set; }
        public string PartnerImage { get; set; }
        public string PartnerName { get; set; }
        public List<string> Items { get; set; }
    }
}
