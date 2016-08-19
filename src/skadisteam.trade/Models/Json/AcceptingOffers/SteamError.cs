using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.AcceptingOffers
{
    public class SteamErrorResponse
    {
        [JsonProperty(PropertyName = "strError")]
        public string StrError { get; set; }
    }
}
