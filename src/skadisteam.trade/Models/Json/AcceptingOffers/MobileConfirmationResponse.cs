using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.AcceptingOffers
{
    public class MobileConfirmationResponse
    {
        [JsonProperty(PropertyName = "tradeid")]
        public long? TradeId { get; set; }
        [JsonProperty(PropertyName = "needs_mobile_confirmation")]
        public bool NeedsMobileConfirmation { get; set; }
        [JsonProperty(PropertyName = "needs_email_confirmation")]
        public bool NeedsEmailConfirmation { get; set; }
        [JsonProperty(PropertyName = "email_domain")]
        public string EmailDomain { get; set; }
    }
}
