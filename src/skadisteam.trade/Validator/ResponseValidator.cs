using System;
using System.Net.Http;

namespace skadisteam.trade.Validator
{

    internal static class ResponseValidator
    {
        internal static bool GetTradeOffersResponse(
            HttpResponseMessage httpResponseMessage)
        {
            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
