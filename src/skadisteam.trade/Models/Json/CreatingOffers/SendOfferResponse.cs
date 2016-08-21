using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.CreatingOffers
{
    internal class SendOfferResponse
    {
        [JsonProperty(PropertyName = "tradeofferid")]
        internal string TradeofferId { get; set; }
        [JsonProperty(PropertyName = "needs_mobile_confirmation")]
        internal bool NeedsMobileConfirmation { get; set; }
        [JsonProperty(PropertyName = "needs_email_confirmation")]
        internal bool NeedsEmailConfirmation { get; set; }
        [JsonProperty(PropertyName = "email_domain")]
        internal string EmailDomain { get; set; }
    }
}
