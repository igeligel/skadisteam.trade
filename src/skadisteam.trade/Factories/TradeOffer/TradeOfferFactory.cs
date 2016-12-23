using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using skadisteam.trade.Converter;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Json;
using skadisteam.trade.Models.TradeOffer;
using AppData = skadisteam.trade.Models.Json.AppData;
using skadisteam.trade.Exceptions;

namespace skadisteam.trade.Factories.TradeOffer
{
    internal static class TradeOfferFactory
    {
        internal static SkadiTradeOffer Create(string content, int id)
        {
            if (content.Contains("This trade offer is no longer valid."))
            {
                throw new OfferNotValidException("This offer is no longer valid. This will be thrown by steam if something is broken or the offer is already accepted/declined");
            }
            var document = new HtmlParser().Parse(content);
            var completeScript = GetScript(document);

            var skadiTradeOffer = new SkadiTradeOffer
            {
                Id = id,
                MyProfile = new SkadiTradeOfferProfile
                {
                    MarketAllowed = GetMarketAllowed(completeScript),
                    ProfileCommunityId = GetProfileCommunityId(content)
                },
                Partner = new SkadiTradeOfferPartner
                {
                    FriendsSince = GetFriendsWithSinceDate(document),
                    PartnerCommunityId = GetPartnerCommunityId(document),
                    PartnerDisplayName = GetPartnerDisplayName(document),
                    PartnerIsMemberSince = GetTradePartnerMemberSince(document),
                    PartnerLevel = GetFriendsPlayerLevel(document),
                    PartnerMiniProfileId = GetPartnerDataMiniprofile(document),
                    PartnerOnlineStatus = GetPartnerOnlineStatus(document),
                    PartnerOnProbation = GetPartnerOnProbation(completeScript)
                },
                SkadiEscrowData = new SkadiEscrowData
                {
                    PartnerEscrowDays = GetDaysTheirEscrow(content),
                    ProfileEscrowDays = GetDaysMyEscrow(content)
                },
                SessionId = GetSessionId(content),
                TradeOfferNote = GetTradeOfferNote(document)
            };


            var myRgAppContextData = GetMyAppDataContext(completeScript);
            var partnerRgAppContextData =
                GetPartnerAppDataContext(completeScript);
            skadiTradeOffer.SkadiAppDataContext = new SkadiAppDataContext
            {
                ProfileAppDataContext =
                    AppContextDataConverter.FromJsonModel(myRgAppContextData),
                PartnerAppDataContext =
                    AppContextDataConverter.FromJsonModel(
                        partnerRgAppContextData)
            };
            skadiTradeOffer.TradeStatus =
                TradeStatusConverter.Convert(
                    GetCurrentTradeStatus(completeScript));
            return skadiTradeOffer;
        }

        private static string GetPartnerDisplayName(IParentNode document)
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

        private static int GetDaysTheirEscrow(string content)
        {
            var daysTheirEscrowString = Regex.Split(content, "var g_daysTheirEscrow = ")[1];
            return int.Parse(Regex.Split(daysTheirEscrowString, ";")[0]);
        }

        private static long GetPartnerCommunityId(IParentNode document)
        {
            return long.Parse(document.QuerySelectorAll(".trade_partner_headline_sub")
                .FirstOrDefault()
                .QuerySelectorAll("a")
                .FirstOrDefault()
                .Attributes.FirstOrDefault(e => e.LocalName == "href")
                .Value.Replace("https://steamcommunity.com/profiles/", ""));
        }

        private static long GetPartnerDataMiniprofile(IParentNode document)
        {
            return
                long.Parse(
                    document.QuerySelectorAll(".trade_partner_headline_sub")
                        .FirstOrDefault()
                        .QuerySelectorAll("a")
                        .FirstOrDefault()
                        .Attributes.FirstOrDefault(
                            e => e.LocalName == "data-miniprofile")
                        .Value);
        }

        private static DateTime GetFriendsWithSinceDate(IParentNode document)
        {
            var friendsSinceText =
               document.QuerySelectorAll(".trade_partner_info_text")
                   .FirstOrDefault()
                   .InnerHtml.RemoveTabs().RemoveNewLines();

            // Datetime
            var provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(friendsSinceText, "dd MMMM, yyyy", provider);
        }

        private static int GetFriendsPlayerLevel(IParentNode document)
        {
            return
                int.Parse(
                    document.QuerySelectorAll(".friendPlayerLevelNum")
                        .FirstOrDefault()
                        .InnerHtml);
        }

        private static DateTime GetTradePartnerMemberSince(IParentNode document)
        {
            var tradePartnerIsMemberSince = document.QuerySelectorAll(".trade_partner_member_since")
                .FirstOrDefault()
                .InnerHtml;
            var provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(tradePartnerIsMemberSince, "dd MMMM, yyyy", provider);
        }

        private static OnlineStatus GetPartnerOnlineStatus(IParentNode document)
        {
            // Convert state to enumeration of offline, online, in-game
            var partnerOnlineStatus =
                document.QuerySelectorAll(".playerAvatar")
                    .LastOrDefault()
                    .ClassList.FirstOrDefault(e => e != "playerAvatar");
            switch (partnerOnlineStatus)
            {
                case "online":
                    return OnlineStatus.Online;
                case "offline":
                    return OnlineStatus.Offline;
                case "in-game":
                    return OnlineStatus.InGame;
                default:
                    return OnlineStatus.Undefined;
            }
        }

        private static string GetTradeOfferNote(IParentNode document)
        {
            var tradeOfferNote =
                document.QuerySelectorAll(".included_trade_offer_note i")
                    .FirstOrDefault().InnerHtml;
            return tradeOfferNote.Contains("&lt;none&gt;")
                ? string.Empty
                : tradeOfferNote;
        }

        private static string GetScript(IParentNode document)
        {
            var completeScript =
                document.QuerySelectorAll("script")
                    .Reverse()
                    .Skip(2)
                    .Take(1)
                    .FirstOrDefault().InnerHtml;
            return completeScript;
        }

        private static Dictionary<string, AppData> GetMyAppDataContext(
            string completeScript)
        {
            var rgAppContextData =
                completeScript.Replace("var g_rgAppContextData = ", "");
            rgAppContextData =
                Regex.Split(rgAppContextData, ";")[0].RemoveNewLines()
                    .RemoveTabs();

            var testString = Regex.Unescape(rgAppContextData);
            return
                JsonConvert.DeserializeObject<Dictionary<string, AppData>>(
                    testString);
        }

        private static Dictionary<string, AppData> GetPartnerAppDataContext(
            string completeScript)
        {
            var partnerRgAppContextData =
                Regex.Split(completeScript, "var g_rgPartnerAppContextData = ")[
                    1];
            partnerRgAppContextData =
                Regex.Split(partnerRgAppContextData, ";")[0];

            var partnerAppDataContext =
                JsonConvert.DeserializeObject<Dictionary<string, AppData>>(
                    partnerRgAppContextData);
            return partnerAppDataContext;
        }

        private static bool GetMarketAllowed(string completeScript)
        {
            var marketAllowed =
                Regex.Split(completeScript, "g_bMarketAllowed = ")[1];
            marketAllowed = Regex.Split(marketAllowed, ";")[0];
            return bool.Parse(marketAllowed);
        }

        private static bool GetPartnerOnProbation(string completeScript)
        {
            var tradePartnerProbation =
                Regex.Split(completeScript, "g_bTradePartnerProbation = ")[1];
            tradePartnerProbation = Regex.Split(tradePartnerProbation, ";")[0];
            return bool.Parse(tradePartnerProbation);
        }

        private static CurrentTradeStatus GetCurrentTradeStatus(string completeScript)
        {
            var rgCurrentTradeStatusText = Regex.Split(completeScript, "var g_rgCurrentTradeStatus = ")[1];
            rgCurrentTradeStatusText = Regex.Split(rgCurrentTradeStatusText, ";")[0];

            var rgCurrentTradeStatus = JsonConvert.DeserializeObject<CurrentTradeStatus>(
                rgCurrentTradeStatusText);
            return rgCurrentTradeStatus;
        }
    }
}
