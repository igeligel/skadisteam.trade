using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using skadisteam.trade.Extensions;

namespace skadisteam.trade.Factories.TradeOffer
{
    internal static class TradeOfferFactory
    {
        internal static void Create(string content)
        {
            var document = new HtmlParser().Parse(content);

            var tradeOfferWithString = GetPartnerDisplayName(document);
            var sessionIdString = GetSessionId(content);
            var profileCommunityId = GetProfileCommunityId(content);
            var daysMyEscrow = GetDaysMyEscrow(content);




            var daysTheirEscrowString = Regex.Split(content, "var g_daysTheirEscrow = ")[1];
            var daysTheirEscrow = int.Parse(Regex.Split(daysTheirEscrowString, ";")[0]);


            var partnerCommunityId = document.QuerySelectorAll(".trade_partner_headline_sub")
                .FirstOrDefault()
                .QuerySelectorAll("a")
                .FirstOrDefault()
                .Attributes.FirstOrDefault(e => e.LocalName == "href")
                .Value.Replace("https://steamcommunity.com/profiles/", "");

            var partnerDataMiniprofile =
                document.QuerySelectorAll(".trade_partner_headline_sub")
                    .FirstOrDefault()
                    .QuerySelectorAll("a")
                    .FirstOrDefault()
                    .Attributes.FirstOrDefault(
                        e => e.LocalName == "data-miniprofile")
                    .Value;

            var friendsSinceText =
                document.QuerySelectorAll(".trade_partner_info_text")
                    .FirstOrDefault()
                    .InnerHtml.RemoveTabs().RemoveNewLines();

            var friendsPlayerLevelText =
                document.QuerySelectorAll(".friendPlayerLevelNum")
                    .FirstOrDefault()
                    .InnerHtml;

            var tradePartnerIsMemberSince =
                document.QuerySelectorAll(".trade_partner_member_since")
                    .FirstOrDefault()
                    .InnerHtml;
        }

        private static string GetPartnerDisplayName(IHtmlDocument document)
        {
            return
                document.QuerySelectorAll("title")
                    .FirstOrDefault()
                    .InnerHtml.Replace("Trade offer with ", "");
        }

        private static string GetSessionId(string content)
        {
            // g_sessionID = "
            var sessionIdString = Regex.Split(content, "g_sessionID = \"")[1];
            var sessionId = Regex.Split(sessionIdString, "\"")[0];
            return sessionId;
        }

        private static long GetProfileCommunityId(string content)
        {
            // g_steamID = "
            var profileCommunityIdString =
                Regex.Split(content, "g_steamID = \"")[1];
            return 
                long.Parse(Regex.Split(profileCommunityIdString, "\"")[0]);
        }

        private static int GetDaysMyEscrow(string content)
        {
            var daysMyEscrowString = Regex.Split(content, "var g_daysMyEscrow = ")[1];
            var daysMyEscrow = int.Parse(Regex.Split(daysMyEscrowString, ";")[0]);
            return daysMyEscrow;
        }
    }
}
