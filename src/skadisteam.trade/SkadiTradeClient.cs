using System;
using System.Collections.Generic;
using System.Linq;
using skadisteam.login.Models;
using skadisteam.trade.Models;
using System.Net;
using System.Net.Http;
using AngleSharp.Parser.Html;
using skadisteam.trade.Extensions;
using skadisteam.trade.Factories;

namespace skadisteam.trade
{
    public class SkadiTradeClient
    {
        private SkadiLoginResponse _skadiLoginResponse;

        public SkadiTradeClient(SkadiLoginResponse skadiLoginResponse)
        {
            _skadiLoginResponse = skadiLoginResponse;
            _skadiLoginResponse.SkadiLoginCookies.AddWebTradeEligilityCookie();
        }

        public List<BasicTradeOffer> GetBasicTradeOffers()
        {
            List<BasicTradeOffer> basicTradeOffers = new List<BasicTradeOffer>();
            // http://steamcommunity.com
            var myTradeOffersPath = "/profiles/" +
                                _skadiLoginResponse.SteamCommunityId +
                                "/tradeoffers/";
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);

            
            HttpResponseMessage response;
            
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://steamcommunity.com");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Pragma", "no-cache");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US;q=0.6,en;q=0.4,it;q=0.2");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", "http://steamcommunity.com/profiles/" + _skadiLoginResponse.SteamCommunityId + "/home");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                client.DefaultRequestHeaders.Host = new Uri("http://steamcommunity.com").Host;
                response = client.GetAsync(myTradeOffersPath).Result;
            }
            var content = response.Content.ReadAsStringAsync().Result;
            var parser = new HtmlParser();
            var document = parser.Parse(content);
            var tradeOffers = document.QuerySelectorAll("div.tradeoffer");
            
            foreach (var tradeOffer in tradeOffers)
            {
                BasicTradeOffer basicTradeOffer = new BasicTradeOffer();
                basicTradeOffer.PartnerItems = new List<BasicTradeOfferItem>();
                basicTradeOffer.ProfileItems = new List<BasicTradeOfferItem>();

                
                basicTradeOffer.Id = int.Parse(tradeOffer.Id.Replace("tradeofferid_", ""));
                var activeClassName =
                    tradeOffer.Children.FirstOrDefault(
                        e => e.ClassList.Contains("tradeoffer_items_ctn"))
                        .ClassName;
                if (activeClassName.Contains("inactive"))
                {
                    basicTradeOffer.Active = false;
                }
                else if (activeClassName.Contains("active"))
                {
                    basicTradeOffer.Active = true;
                }
                basicTradeOffers.Add(basicTradeOffer);
            }
            return basicTradeOffers;

        }
    }
}
