using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.BasicTradeOffer
{
    public class BasicTradeOfferItem
    {
        public IDataEconomy DataEconomy { get; set; }

        public string ItemPicture { get; set; }

        public bool Missing { get; set; }
    }
}
