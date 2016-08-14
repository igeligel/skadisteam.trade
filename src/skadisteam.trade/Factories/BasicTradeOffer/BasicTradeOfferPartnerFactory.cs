using AngleSharp.Dom;
using skadisteam.trade.Models;
using System.Linq;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models.BasicTradeOffer;
using Regex = System.Text.RegularExpressions.Regex;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferPartnerFactory
    {
        internal static BasicTradeOfferPartner Create(IElement angleSharpElement)
        {
            var basicTradeOfferPartner = new BasicTradeOfferPartner
            {
                SteamCommunityId = CreateSteamCommunityId(angleSharpElement),
                OnlineStatus = CreateVisiblityState(angleSharpElement),
                MiniProfileId = CreateMiniProfileId(angleSharpElement),
                AvatarUrl = CreateAvatarUrl(angleSharpElement),
                DisplayName = CreateDisplayName(angleSharpElement)
            };
            return basicTradeOfferPartner;
        }

        private static long CreateSteamCommunityId(IParentNode angleSharpElement)
        {
            var steamidString =
                angleSharpElement.QuerySelectorAll(Html.Anchor)
                    .FirstOrDefault()
                    .Attributes.FirstOrDefault(e => e.Name == Html.Link)
                    .Value;

            steamidString =
                Regex.Split(steamidString, RegexPatterns.CommunityId)[1];
            steamidString =
                Regex.Split(steamidString, RegexPatterns.SingleQuote)[0];
            return long.Parse(steamidString);
        }

        private static OnlineStatus CreateVisiblityState(IParentNode angleSharpElement)
        {
            var visibilityState =
                angleSharpElement.QuerySelectorAll(
                    HtmlQuerySelectors.PlayerAvatarClass)
                    .FirstOrDefault()
                    .ClassList.FirstOrDefault(e => e != HtmlClasses.PlayerAvatar);

            switch (visibilityState)
            {
                case HtmlClasses.Online:
                    return OnlineStatus.Online;
                case HtmlClasses.Offline:
                    return OnlineStatus.Offline;
                case HtmlClasses.InGame:
                    return OnlineStatus.InGame;
                // ReSharper disable once RedundantEmptyDefaultSwitchBranch
                default:
                    return OnlineStatus.Undefined;
            }
        }

        private static int CreateMiniProfileId(IParentNode angleSharpElement)
        {
            return
                int.Parse(
                    angleSharpElement.QuerySelectorAll(
                        HtmlQuerySelectors.PlayerAvatarClass)
                        .FirstOrDefault()
                        .Attributes.FirstOrDefault(
                            e => e.Name == HtmlAttributes.DataMiniprofile)
                        .Value);
        }

        private static string CreateAvatarUrl(IParentNode angleSharpElement)
        {
            return
                angleSharpElement.QuerySelectorAll(
                    HtmlQuerySelectors.PlayerAvatarClass)
                    .FirstOrDefault()
                    .QuerySelectorAll(Html.Image)
                    .FirstOrDefault()
                    .Attributes.FirstOrDefault(
                        e => e.Name == HtmlAttributes.Source)
                    .Value;
        }

        private static string CreateDisplayName(IParentNode angleSharpElement)
        {
            return angleSharpElement.QuerySelectorAll(
                    HtmlQuerySelectors.TradeofferHeader)
                    .FirstOrDefault()
                    .TextContent.RemoveNewLines()
                    .RemoveTabs()
                    .RemoveOfferingTradeMessage();
        }
    }
}
