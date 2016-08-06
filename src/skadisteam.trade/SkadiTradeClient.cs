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
    }
}
