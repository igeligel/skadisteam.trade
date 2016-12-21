using Newtonsoft.Json;
using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.Json.AcceptingOffers
{
    public class SteamErrorResponse : IAcceptOfferResponse
    {
        [JsonProperty(PropertyName = "strError")]
        public string StrError { get; set; }
    }
}
