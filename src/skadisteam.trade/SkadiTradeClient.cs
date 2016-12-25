using System;
using System.Collections.Generic;
using System.Linq;
using skadisteam.login.Models;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;
using skadisteam.trade.Factories;
using skadisteam.trade.Factories.BasicTradeOffer;
using skadisteam.trade.Factories.TradeOffer;
using skadisteam.trade.Interfaces;
using skadisteam.trade.Models.BasicTradeOffer;
using skadisteam.trade.Models.Json.CreatingOffers;
using skadisteam.trade.Models.TradeOffer;
using skadisteam.trade.Validator;
using System.Net.Http;

namespace skadisteam.trade
{
    public class SkadiTradeClient
    {
        private readonly SkadiLoginResponse _skadiLoginResponse;
        private readonly string _deviceId;
        private readonly string _identitySecret;

        public SkadiTradeClient(SkadiLoginResponse skadiLoginResponse)
        {
            _skadiLoginResponse = skadiLoginResponse;
            _skadiLoginResponse.SkadiLoginCookies.AddWebTradeEligilityCookie();
        }

        public SkadiTradeClient(SkadiLoginResponse skadiLoginResponse, string deviceId, string identitySecret)
            :this(skadiLoginResponse)
        {
            _deviceId = deviceId;
            _identitySecret = identitySecret;
        }

        public List<BasicTradeOffer> GetBasicTradeOffers()
        {
            var myTradeOffersPath =
                UrlPathFactory.GetBasicTradeOffer(
                    _skadiLoginResponse.SteamCommunityId);
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);

            var response =
                RequestFactory.GetTradeOffersListResponseMessage(handler,
                    myTradeOffersPath, _skadiLoginResponse.SteamCommunityId);

            // ReSharper disable once UnusedVariable
            // TODO: Check responses if they are valid.
            var valid = ResponseValidator.GetTradeOffersResponse(response);

            var content = response.Content.ReadAsStringAsync().Result;
            var document = new HtmlParser().Parse(content);

            var tradeOffers =
                document.QuerySelectorAll(HtmlQuerySelectors.TradeOffer);

            return tradeOffers.Select(BasicTradeOfferFactory.Create).ToList();
        }

        public SkadiTradeOffer LoadTradeOffer(int id)
        {
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var response = RequestFactory.GetTradeOfferResponseMessage(handler,
                UrlPathFactory.TradeOffer(id), _skadiLoginResponse.SteamCommunityId);
            var content = response.Content.ReadAsStringAsync().Result;
            return TradeOfferFactory.Create(content, id);
        }

        public void DeclineOffer(SkadiTradeOffer skadiTradeOffer)
        {
            var httpClientHandler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            RequestFactory.DeclineOffer(httpClientHandler, skadiTradeOffer,
                UrlPathFactory.DeclineOffer(skadiTradeOffer.Id));
        }

        public IAcceptOfferResponse AcceptOffer(SkadiTradeOffer skadiTradeOffer)
        {
            var httpClientHandler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var response = RequestFactory.AcceptOffer(httpClientHandler,
                skadiTradeOffer, UrlPathFactory.AcceptOffer(skadiTradeOffer.Id));
            var responseBody = response.Content.ReadAsStringAsync().Result;
            return OfferResponseFactory.Create(responseBody);
        }
        
        public void SendTradeOffer(List<Asset> myAssets, List<Asset> partnerAssets, long partnerCommunityId, string tradeOfferToken, string tradeOfferMessage)
        {
            var createOffer = CreateOfferModelFactory.Create(myAssets,
                partnerAssets);

            var tradeOfferCreateParameter = new TradeOfferCreateParameter
            {
                TradeOfferAccessToken = tradeOfferToken
            };

            var httpClientHandler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var response = RequestFactory.CreateTradeOffer(httpClientHandler,
                createOffer, partnerCommunityId, _skadiLoginResponse.SessionId,
                tradeOfferCreateParameter, tradeOfferMessage, tradeOfferToken);
            var responseBody = response.Content.ReadAsStringAsync().Result;
            // ReSharper disable once UnusedVariable
            // TODO: Check response for errors.
            var sendOfferResponse =
                JsonConvert.DeserializeObject<SendOfferResponse>(responseBody);
        }

        private void AcceptMobileConfirmation(IMobileConfirmation mobileConfirmation)
        {
            var referer = MobileConfirmationFactory.GenerateConfirmationUrl(
                _deviceId, _identitySecret, _skadiLoginResponse.SteamCommunityId);
            if (string.IsNullOrEmpty(_deviceId) || string.IsNullOrEmpty(_identitySecret))
            {
                throw new ArgumentException("You have not set the device id and/or the identity secret which is required for this functionality." +
                    "To fix this please use a constructor of this class where the device id and the identity secret is required as parameter.");
            }
            var urlToConfirm = "/mobileconf/ajaxop?op=allow&" +
                                   MobileConfirmationFactory
                                       .GenerateConfirmationQueryParams(
                                           "allow", _deviceId, _identitySecret,
                                           _skadiLoginResponse.SteamCommunityId) +
                                   "&cid=" + mobileConfirmation.Id + "&ck=" + mobileConfirmation.Key;

            HttpClientHandler handler =
            HttpClientHandlerFactory.CreateWithCookieContainer(
                _skadiLoginResponse.SkadiLoginCookies);
            RequestFactory.ApproveConfirmations(handler, urlToConfirm, referer,
                _skadiLoginResponse.SteamCommunityId);
        }

        public List<IMobileConfirmation> GetConfirmations()
        {
            var url = MobileConfirmationFactory.GenerateConfirmationUrl(
                _deviceId, _identitySecret, _skadiLoginResponse.SteamCommunityId);
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var confirmationDataResponse =
                RequestFactory.GetConfirmationData(handler, url,
                    _skadiLoginResponse.SteamCommunityId);
            var confirmationDataContent =
                confirmationDataResponse.Content.ReadAsStringAsync().Result;
            var mobileConfirmations = MobileConfirmationFactory.CreateConfirmationList(confirmationDataContent);
            return mobileConfirmations;
        }
        
        public void ConfirmAllTrades(string deviceId, string identitySecret)
        {
            var mobileConfirmations = GetConfirmations();
            foreach (var mobileConfirmation in mobileConfirmations)
            {
                AcceptMobileConfirmation(mobileConfirmation);
            }
        }
    }
}
