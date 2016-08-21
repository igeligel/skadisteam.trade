using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using skadisteam.login.Models;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;
using skadisteam.trade.Factories;
using skadisteam.trade.Factories.BasicTradeOffer;
using skadisteam.trade.Factories.TradeOffer;
using skadisteam.trade.Models;
using skadisteam.trade.Models.BasicTradeOffer;
using skadisteam.trade.Models.Json.AcceptingOffers;
using skadisteam.trade.Models.Json.CreatingOffers;
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

        public void SendTradeOffer(List<Asset> myAssets, List<Asset> partnerAssets, long partnerCommunityId, string tradeOfferToken, string tradeOfferMessage)
        {
            var createOffer = CreateOfferModelFactory.Create(myAssets,
                partnerAssets);

            JsonConvert.SerializeObject(createOffer);

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

        public TradeReceiptResponse AcceptOffer(SkadiTradeOffer skadiTradeOffer)
        {
            var path = UrlPathFactory.AcceptOffer(skadiTradeOffer.Id);
            var httpClientHandler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var response = RequestFactory.AcceptOffer(httpClientHandler,
                skadiTradeOffer, path);
            var responseBody = response.Content.ReadAsStringAsync().Result;

            // ReSharper disable once NotAccessedVariable
            // TODO: Create model to return.
            MobileConfirmationResponse mobileConfirmationResponse = null;
            TradeReceiptResponse tradeReceiptResponse = null;
            SteamErrorResponse steamErrorResponse = null;
            try
            {
                // ReSharper disable once RedundantAssignment
                mobileConfirmationResponse =
                    JsonConvert.DeserializeObject<MobileConfirmationResponse>(
                        responseBody);
            }
            catch (JsonSerializationException)
            {

            }

            try
            {
                tradeReceiptResponse =
                    JsonConvert.DeserializeObject<TradeReceiptResponse>(
                        responseBody);
            }
            catch (JsonSerializationException)
            {

            }

            try
            {
                steamErrorResponse =
                    JsonConvert.DeserializeObject<SteamErrorResponse>(
                        responseBody);

            }
            catch (JsonSerializationException)
            {
            }

            if (steamErrorResponse != null)
            {
                var errorCode =
                    int.Parse(
                        steamErrorResponse.StrError.Replace(
                            "There was an error accepting this trade offer.  Please try again later. (",
                            "").Replace(")", ""));
                // ReSharper disable once UnusedVariable
                // TODO: implement model to return.
                var errEnum = (SteamError)errorCode;
            }

            return tradeReceiptResponse;
        }

        public void ConfirmAllTrades(string deviceId, string identitySecret)
        {
            var url = MobileConfirmationFactory.GenerateConfirmationUrl(
                deviceId, identitySecret, _skadiLoginResponse.SteamCommunityId);
            var handler =
                HttpClientHandlerFactory.CreateWithCookieContainer(
                    _skadiLoginResponse.SkadiLoginCookies);
            var confirmationDataResponse =
                RequestFactory.GetConfirmationData(handler, url,
                    _skadiLoginResponse.SteamCommunityId);
            var confirmationDataContent =
                confirmationDataResponse.Content.ReadAsStringAsync().Result;
            var mobileConfList = Regex.Split(confirmationDataContent,
                "<div class=\"mobileconf_list_entry\"");
            if (mobileConfList.Length == 1) return;
            for (var i = 1; i < mobileConfList.Length; i++)
            {
                var dataConfigId =
                    Regex.Split(mobileConfList[i], "data-confid=\"")[1];
                dataConfigId = Regex.Split(dataConfigId, "\"")[0];
                var dataKey = Regex.Split(mobileConfList[i], "data-key=\"")[1];
                dataKey = Regex.Split(dataKey, "\"")[0];
                var urlToConfirm = "/mobileconf/ajaxop?op=allow&" +
                                   MobileConfirmationFactory
                                       .GenerateConfirmationQueryParams(
                                           "allow", deviceId, identitySecret,
                                           _skadiLoginResponse.SteamCommunityId) +
                                   "&cid=" + dataConfigId + "&ck=" + dataKey;
                RequestFactory.ApproveConfirmations(handler, urlToConfirm, url,
                    _skadiLoginResponse.SteamCommunityId);
            }
        }
    }
}
