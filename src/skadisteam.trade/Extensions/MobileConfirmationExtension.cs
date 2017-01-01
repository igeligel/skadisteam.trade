using System.Text.RegularExpressions;
using AngleSharp.Dom;
using skadisteam.trade.Models.Confirmation;
using System.Linq;
using skadisteam.trade.Factories;

namespace skadisteam.trade.Extensions
{
    internal static class MobileConfirmationExtension
    {
        internal static TradeConfirmation ToTradeConfirmation(this MobileConfirmation mobileConfirmation, IElement domElement)
        {
            var tradeConfirmation = new TradeConfirmation
            {
                Id = mobileConfirmation.Id,
                Key = mobileConfirmation.Key,
                Creator = mobileConfirmation.Creator,
                Time = mobileConfirmation.Time
            };
            tradeConfirmation.PartnerOnlineStatus =
                OnlineStatusFactory.Create(domElement);
            tradeConfirmation.PartnerImage =
                domElement.QuerySelector(".playerAvatar img")
                    .Attributes.FirstOrDefault(e => e.Name == "src").Value;

            tradeConfirmation.PartnerName = domElement.QuerySelector(
                    ".mobileconf_list_entry_description").Children[0].TextContent
                .Replace("Trade with ", string.Empty);

            var itemsContent = domElement.QuerySelector(
                ".mobileconf_list_entry_description").Children[1].TextContent;

            tradeConfirmation.Items = Regex.Split(itemsContent, ", ").ToList();
            return tradeConfirmation;
        }

        internal static MarketListingConfirmation ToMarketListingConfirmation(this MobileConfirmation mobileConfirmation, IElement domElement)
        {
            var marketListingConfirmation = new MarketListingConfirmation
            {
                Id = mobileConfirmation.Id,
                Key = mobileConfirmation.Key,
                Creator = mobileConfirmation.Creator,
                Time = mobileConfirmation.Time
            };
            marketListingConfirmation.Item = domElement.QuerySelector(
                    ".mobileconf_list_entry_description")
                .Children[0].InnerHtml.Replace("Sell - ", "");
            var priceElement =
                domElement.QuerySelector(".mobileconf_list_entry_description")
                    .Children[1].InnerHtml;
            var openedParentheses = Regex.Escape(" (");
            var currency = Regex.Split(priceElement, openedParentheses)[0];
            marketListingConfirmation.Currency =
                currency[currency.Length - 1].ToString();
            var normalPrice =
                currency.Substring(1, currency.Length - 2).Replace(",", ".");
            marketListingConfirmation.SellPrice = decimal.Parse(normalPrice);
            var taxPrice =
                Regex.Split(priceElement, openedParentheses)[1];
            taxPrice = Regex.Split(taxPrice, "\\S\\)")[0].Replace(",", ".");
            marketListingConfirmation.SellPriceAfterTaxes =
                decimal.Parse(taxPrice);
            return marketListingConfirmation;
        }
    }
}
