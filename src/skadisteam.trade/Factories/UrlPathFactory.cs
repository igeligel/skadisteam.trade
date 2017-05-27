using skadisteam.trade.Models;

namespace skadisteam.trade.Factories
{
    internal static class UrlPathFactory
    {
        internal static string GetBasicTradeOffer(long steamCommunityId)
        {
            return "/profiles/" + steamCommunityId + "/tradeoffers/";
        }

        internal static string TradeOffer(long id)
        {
            return "/tradeoffer/" + id + "/";
        }

        internal static string DeclineOffer(long id)
        {
            return "/tradeoffer/" + id + "/decline";
        }

        internal static string AcceptOffer(long id)
        {
            return "/tradeoffer/" + id + "/accept";
        }

        internal static string ConfirmationUrl(ConfirmationUrlParameter confirmationUrlParameter)
        {
            return "/mobileconf/ajaxop?op=allow&" +
                                   MobileConfirmationFactory
                                       .GenerateConfirmationQueryParams(
                                           confirmationUrlParameter.ConfirmationTag, confirmationUrlParameter.DeviceId, confirmationUrlParameter.IdentitySecret,
                                           confirmationUrlParameter.SteamCommunityId) +
                                   "&cid=" + confirmationUrlParameter.MobileConfirmation.Id + "&ck=" + confirmationUrlParameter.MobileConfirmation.Key;
        }
    }
}
