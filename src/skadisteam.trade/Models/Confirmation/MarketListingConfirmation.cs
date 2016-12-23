namespace skadisteam.trade.Models.Confirmation
{
    public class MarketListingConfirmation:MobileConfirmation
    {
        public string Item { get; set; }
        public decimal SellPrice { get; set; }
        public decimal SellPriceAfterTaxes { get; set; }
        public string Currency { get; set; }
    }
}
