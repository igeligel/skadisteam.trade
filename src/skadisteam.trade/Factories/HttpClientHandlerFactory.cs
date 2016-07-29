using System.Net;
using System.Net.Http;

namespace skadisteam.trade.Factories
{
    internal static class HttpClientHandlerFactory
    {
        internal static HttpClientHandler CreateWithCookieContainer(CookieContainer cookieContainer)
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                AutomaticDecompression = DecompressionMethods.GZip |
                                         DecompressionMethods.Deflate
            };
            return handler;
        }
    }
}
