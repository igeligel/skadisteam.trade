using skadisteam.trade.Constants;

namespace skadisteam.trade.Factories
{
    internal class UrlFactory
    {
        internal static string SteamCommunityHome(long steamCommunityId)
        {
            return Urls.SteamCommunityBase + "/profiles/" + steamCommunityId +
                   "/home";
        }

        internal static string TradeOfferPage(long steamCommunityId)
        {
            return Urls.SteamCommunityBase + "/profiles/" + steamCommunityId +
                   "/tradeoffers/";
        }

        internal static string TradeOffer(int id)
        {
            return Urls.SteamCommunityBaseSecured + "/tradeoffer/" + id + "/";
        }

        internal static string TradeOfferExecution(long partnerCommunityId,
            string tradeOfferToken)
        {
            return Urls.SteamCommunityBaseSecured + "/tradeoffer/new/?partner=" +
                   (partnerCommunityId - SteamIds.SteamCommunityBaseId) +
                   "&token=" +
                   tradeOfferToken;
        }
    }
}
