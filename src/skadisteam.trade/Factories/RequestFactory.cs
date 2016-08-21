using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models.Json.CreatingOffers;
using skadisteam.trade.Models.TradeOffer;

namespace skadisteam.trade.Factories
{
    internal static class RequestFactory
    {
        internal static HttpResponseMessage GetTradeOffersListResponseMessage(
            HttpClientHandler httpClientHandler, string path,
            long steamCommunityId)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = Uris.SteamCommunityBase;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.CacheControl, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Pragma, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept,
                    HttpHeaderValues.AcceptHtmlImagesAndXml);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent, HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    UrlFactory.SteamCommunityHome(steamCommunityId));
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UpgradeInsecureRequest, HttpHeaderValues.One);
                client.DefaultRequestHeaders.Host = Uris.SteamCommunityBase.Host;
                response = client.GetAsync(path).Result;
            }
            return response;
        }

        internal static HttpResponseMessage GetTradeOfferResponseMessage(
            HttpClientHandler httpClientHandler, string path,
            long steamCommunityId)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept,
                    HttpHeaderValues.AcceptHtmlImagesAndXml);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateSdchOrBr);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent, HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    UrlFactory.TradeOfferPage(steamCommunityId));
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UpgradeInsecureRequest, HttpHeaderValues.One);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                response = client.GetAsync(path).Result;
            }
            return response;
        }

        internal static HttpResponseMessage GetConfirmationData(HttpClientHandler httpClientHandler, string path, long steamCommunityId)
        {
            httpClientHandler.CookieContainer.EnableMobileCookieContainer(
                steamCommunityId);
            HttpResponseMessage httpResponseMessage;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept,
                    HttpHeaderValues.AcceptHtmlImagesAndXml);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageGermanEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent, HttpHeaderValues.MobileUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.XrequestedWith,
                    HttpHeaderValues.RequestedWithAndroidApp);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UpgradeInsecureRequest, HttpHeaderValues.One);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                httpResponseMessage = client.GetAsync(path).Result;
            }
            httpClientHandler.CookieContainer.DisableMobileCookieContainer();
            return httpResponseMessage;
        }

        internal static void ApproveConfirmations(HttpClientHandler httpClientHandler, string path, string referer, long steamCommunityId)
        {
            httpClientHandler.CookieContainer.EnableMobileCookieContainer(
                steamCommunityId);
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept, HttpHeaderValues.AcceptAll);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageGermanEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent,
                    HttpHeaderValues.MobileUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.XrequestedWith,
                    HttpHeaderValues.RequestedWithXmlHttpRequest);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    referer);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                client.GetAsync(path).Wait();
            }
            httpClientHandler.CookieContainer.DisableMobileCookieContainer();
        }

        internal static void DeclineOffer(HttpClientHandler httpClientHandler, SkadiTradeOffer skadiTradeOffer, string path)
        {
            using (var client = new HttpClient(httpClientHandler))
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("sessionid",
                        skadiTradeOffer.SessionId)
                });
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept,
                    HttpHeaderValues.AcceptAll);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrBr);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent,
                    HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    UrlFactory.TradeOffer(skadiTradeOffer.Id));
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Origin,
                    Urls.SteamCommunityBaseSecured);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                client.PostAsync(path, content).Wait();
            }
        }

        internal static HttpResponseMessage AcceptOffer(HttpClientHandler httpClientHandler, SkadiTradeOffer skadiTradeOffer, string path)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("sessionid",
                        skadiTradeOffer.SessionId),
                    new KeyValuePair<string, string>("serverid", "1"),
                    new KeyValuePair<string, string>("tradeofferid",
                        skadiTradeOffer.Id.ToString()),
                    new KeyValuePair<string, string>("partner",
                        skadiTradeOffer.Partner.PartnerCommunityId.ToString()),
                    new KeyValuePair<string, string>("captcha", "")
                });
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept, HttpHeaderValues.AcceptAll);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrBr);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent,
                    HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    UrlFactory.TradeOffer(skadiTradeOffer.Id));
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Origin,
                    Urls.SteamCommunityBaseSecured);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                response = client.PostAsync(path, content).Result;
            }
            return response;
        }

        internal static HttpResponseMessage CreateTradeOffer(HttpClientHandler httpClientHandler, CreateOffer createOffer, long partnerCommunityId, string sessionId, TradeOfferCreateParameter tradeOfferCreateParameter, string tradeOfferMessage, string tradeOfferToken)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("captcha",""),
                    new KeyValuePair<string, string>("json_tradeoffer", JsonConvert.SerializeObject(createOffer)),
                    new KeyValuePair<string, string>("partner", partnerCommunityId.ToString()),
                    new KeyValuePair<string, string>("serverid", "1"),
                    new KeyValuePair<string, string>("sessionid", sessionId),
                    new KeyValuePair<string, string>("trade_offer_create_params", JsonConvert.SerializeObject(tradeOfferCreateParameter)),
                    new KeyValuePair<string, string>("tradeoffermessage", tradeOfferMessage)
                });
                client.BaseAddress = Uris.SteamCommunityBaseSecured;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept, HttpHeaderValues.AcceptAll);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrBr);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent, HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    UrlFactory.TradeOfferExecutrion(partnerCommunityId,
                        tradeOfferToken));
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Origin, Urls.SteamCommunityBaseSecured);
                client.DefaultRequestHeaders.Host =
                    Uris.SteamCommunityBaseSecured.Host;
                response =
                    client.PostAsync(UrlPaths.SendTradeOffer, content).Result;
            }
            return response;
        }
    }
}
