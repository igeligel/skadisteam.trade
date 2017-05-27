using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferFactory
    {
        internal static Models.BasicTradeOffer.BasicTradeOffer Create(IElement angleSharpElement)
        {
            var tradeOfferItems =
                angleSharpElement.QuerySelectorAll(HtmlQuerySelectors.TradeOfferItemList);
            var basicTradeOffer = new Models.BasicTradeOffer.BasicTradeOffer
            {
                Partner =
                    BasicTradeOfferPartnerFactory.Create(angleSharpElement),
                ProfileItems =
                    BasicTradeOfferItemListFactory.CreateProfileItems(
                        tradeOfferItems),
                PartnerItems =
                    BasicTradeOfferItemListFactory.CreatePartnerItems(
                        tradeOfferItems),
                Id = GetTradeOfferId(angleSharpElement),
                Active = TradeOfferActive(angleSharpElement)
            };
            if (basicTradeOffer.Active)
            {
                basicTradeOffer.ExpiringDate = GetExpireDate(angleSharpElement);
            }
            return basicTradeOffer;
        }

        private static long GetTradeOfferId(IElement angleSharpElement)
        {
            return
                long.Parse(
                    angleSharpElement.Id.Replace(
                        RegexPatterns.TradeOfferIdUnderscore, string.Empty));
        }

        private static bool TradeOfferActive(IParentNode angleSharpElement)
        {
            var activeClassName =
                angleSharpElement.Children.FirstOrDefault(
                    e => e.ClassList.Contains(HtmlClasses.TradeOfferItemsCount))
                    .ClassName;
            return activeClassName.Contains(HtmlClasses.Active);
        }

        private static DateTime GetExpireDate(IParentNode angleSharpElement)
        {
            var htmlElementTextContent = angleSharpElement.LastElementChild.TextContent;
            if (!htmlElementTextContent.Contains(RegexPatterns.OfferExpireson)) return DateTime.MinValue;
            var splittedText = Regex.Split(htmlElementTextContent, RegexPatterns.OfferExpireson)[1];
            splittedText = splittedText.RemoveNewLines().RemoveTabs();
            if (DateTime.TryParseExact(splittedText, DateTimeFormats.TradeOfferSimpleDate, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime expireTime))
            {
                return expireTime;
            }
            if (DateTime.TryParseExact(splittedText, DateTimeFormats.TradeOfferSimpleDateShort, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out expireTime))
            {
                return expireTime;
            }
            return DateTime.MinValue;
        }
    }
}
