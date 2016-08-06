using System;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferFactory
    {
        internal static Models.BasicTradeOffer Create(IElement angleSharpElement)
        {
            var tradeOfferItems =
                angleSharpElement.QuerySelectorAll(HtmlQuerySelectors.TradeOfferItemList);
            var basicTradeOffer = new Models.BasicTradeOffer
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
                Active = TradeOfferActive(angleSharpElement),
                ExpiringDate = GetExpireDate(angleSharpElement)
            };
            return basicTradeOffer;
        }

        private static int GetTradeOfferId(IElement angleSharpElement)
        {
            return
                int.Parse(
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
            var test = angleSharpElement.LastElementChild.TextContent;
            var bla = Regex.Split(test, RegexPatterns.OfferExpireson)[1];
            bla = bla.RemoveNewLines().RemoveTabs();
            var expireTime = DateTime.ParseExact(bla,
                DateTimeFormats.TradeOfferSimpleDate,
                System.Globalization.CultureInfo.InvariantCulture);
            return expireTime;
        }
    }
}
