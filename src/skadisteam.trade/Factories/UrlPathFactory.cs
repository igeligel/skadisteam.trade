namespace skadisteam.trade.Factories
{
    internal static class UrlPathFactory
    {
        internal static string GetBasicTradeOffer(long steamCommunityId)
        {
            return "/profiles/" + steamCommunityId + "/tradeoffers/";
        }

        internal static string TradeOffer(int id)
        {
            return "/tradeoffer/" + id + "/";
        }

        internal static string DeclineOffer(int id)
        {
            return "/tradeoffer/" + id + "/decline";
        }

        internal static string AcceptOffer(int id)
        {
            return "/tradeoffer/" + id + "/accept";
        }
    }
}
