using System;
using System.Collections.Generic;
using System.Linq;
using skadisteam.login.Models;
using skadisteam.trade.Models;
using System.Net;
using System.Net.Http;
using AngleSharp.Parser.Html;
using skadisteam.trade.Constants;
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

            var response = RequestFactory.GetTradeOfferResponseMessage(handler,
                myTradeOffersPath, _skadiLoginResponse.SteamCommunityId);

            var content = response.Content.ReadAsStringAsync().Result;
            var parser = new HtmlParser();
            var document = parser.Parse(content);
            var tradeOffers = document.QuerySelectorAll("div.tradeoffer");
            
            foreach (var tradeOffer in tradeOffers)
            {
                var tradeOfferItems =
                    tradeOffer.QuerySelectorAll(".tradeoffer_item_list");
                var profileItems = tradeOfferItems.FirstOrDefault();
                var partnerItems = tradeOfferItems.LastOrDefault();

                var partnerItemInstances =
                    partnerItems.QuerySelectorAll(".trade_item");
                foreach (var partnerItemInstance in partnerItemInstances)
                {
                    BasicTradeOfferItem basicTradeOfferItem =
                        new BasicTradeOfferItem();

                    var dataEconomyItem =
                        partnerItemInstance.Attributes.FirstOrDefault(
                            e => e.Name == "data-economy-item").Value;
                    basicTradeOfferItem.ItemPicture =
                        partnerItemInstance.Children.FirstOrDefault(
                            e => e.TagName == "IMG")
                            .Attributes.FirstOrDefault(e => e.Name == "src")
                            .Value;
                }

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
