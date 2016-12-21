using Newtonsoft.Json;

namespace skadisteam.trade.Converter
{
    internal static class JsonToAcceptOfferResponse
    {
        internal static object ParseAcceptOffer<T>(string responseBody)
        {
            try
            {
                return
                    JsonConvert.DeserializeObject<T>(
                        responseBody);
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }
    }
}
