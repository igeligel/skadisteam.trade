using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using skadisteam.login.Models;
using skadisteam.trade.Models;
using AngleSharp.Parser.Html;
using skadisteam.trade.Extensions;
using skadisteam.trade.Factories;
using skadisteam.trade.Factories.BasicTradeOffer;
using skadisteam.trade.Factories.TradeOffer;
using skadisteam.trade.Models.BasicTradeOffer;
using skadisteam.trade.Models.TradeOffer;
using skadisteam.trade.Validator;

namespace skadisteam.trade
{
    public class SkadiTradeClient
    {
        private readonly SkadiLoginResponse _skadiLoginResponse;

        public SkadiTradeClient(SkadiLoginResponse skadiLoginResponse)
        {
            _skadiLoginResponse = skadiLoginResponse;
            _skadiLoginResponse.SkadiLoginCookies.AddWebTradeEligilityCookie();
        }

        public List<BasicTradeOffer> GetBasicTradeOffers()
        {
            // http://steamcommunity.com
            var myTradeOffersPath = "/profiles/" +
                                _skadiLoginResponse.SteamCommunityId +
                                "/tradeoffers/";
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);

            var response =
                RequestFactory.GetTradeOffersListResponseMessage(handler,
                    myTradeOffersPath, _skadiLoginResponse.SteamCommunityId);

            var valid = ResponseValidator.GetTradeOffersResponse(response);

            var content = response.Content.ReadAsStringAsync().Result;
            var document = new HtmlParser().Parse(content);

            var tradeOffers = document.QuerySelectorAll("div.tradeoffer");

            return tradeOffers.Select(BasicTradeOfferFactory.Create).ToList();
        }
        
        public SkadiTradeOffer LoadTradeOffer(int id, long communityId)
        {
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var response = RequestFactory.GetTradeOfferResponseMessage(handler,
                "/tradeoffer/"+ id + "/", communityId);
            var content = response.Content.ReadAsStringAsync().Result;
            return TradeOfferFactory.Create(content);
        }
    }
}
