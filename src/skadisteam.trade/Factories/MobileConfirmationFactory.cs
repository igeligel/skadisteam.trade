using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using AngleSharp.Dom;
using skadisteam.trade.Interfaces;
using System.Linq;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models.Confirmation;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AngleSharp.Parser.Html;

namespace skadisteam.trade.Factories
{
    public static class MobileConfirmationFactory
    {
        private static DateTime ParseTime(string timeText)
        {
            if (timeText == "Just now")
            {
                return DateTime.UtcNow;
            }
            if (timeText.Contains(" minute ago"))
            {
                return DateTime.UtcNow.AddMinutes(-1);
            }
            if (timeText.Contains(" minutes ago"))
            {
                var timeToSubtract = int.Parse(Regex.Split(timeText, " minutes ago")[0]);
                return DateTime.UtcNow.AddMinutes(-timeToSubtract);
            }
            if (timeText.Contains(" hour ago"))
            {
                return DateTime.UtcNow.AddHours(-1);
            }
            if (timeText.Contains(" hours ago"))
            {
                var timeToSubtract = int.Parse(Regex.Split(timeText, " hours ago")[0]);
                return DateTime.UtcNow.AddHours(-timeToSubtract);
            }
            if (timeText.Contains(" day ago"))
            {
                return DateTime.UtcNow.AddDays(-1);
            }
            if (timeText.Contains(" days ago"))
            {
                var timeToSubtract = int.Parse(Regex.Split(timeText, " days ago")[0]);
                return DateTime.UtcNow.AddDays(-timeToSubtract);
            }
            return DateTime.MinValue;
        }
        internal static IMobileConfirmation Create(IElement domElement)
        {
            if (domElement == null) return null;
            var id = long.Parse(domElement.Attributes.FirstOrDefault(
                    e => e.Name == "data-confid").Value);
            var key =
                    ulong.Parse(domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-key").Value);
            var creator =
                    int.Parse(domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-creator").Value);
            var type =
                (ConfirmationType)
                int.Parse(
                    domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-type").Value);
            // mobileconf_list_entry_description
            var timeText = domElement.QuerySelector(
                    ".mobileconf_list_entry_description").Children[2].TextContent;
            var time = ParseTime(timeText);

            var mobileConfirmation = new MobileConfirmation
            {
                Id = id,
                Creator = creator,
                Key = key,
                Time = time
            };
            
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (type)
            {
                case ConfirmationType.ConfirmTrade:
                    var tradeConfirmation =
                        mobileConfirmation.ToTradeConfirmation(domElement);
                    return tradeConfirmation;
                case ConfirmationType.CreateListing:
                    var marketListingConfirmation =
                        new MarketListingConfirmation();
                    return marketListingConfirmation;
            }
            return mobileConfirmation;
        }

        internal static string GenerateConfirmationUrl(string devideId, string identitySecret, long steamCommunityId)
        {
            const string endpoint = "/mobileconf/conf?";
            var queryString = GenerateConfirmationQueryParams(ConfirmationTag.Confirm, devideId, identitySecret, steamCommunityId);
            return endpoint + queryString;
        }

        internal static string GenerateConfirmationQueryParams(ConfirmationTag tag, string deviceId, string identitySecret, long steamCommunityId)
        {
            var time = TimeAligner.GetSteamTime();
            return "p=" + deviceId + "&a=" + steamCommunityId + "&k=" +
                   _generateConfirmationHashForTime(identitySecret, time,
                       tag.ToText()) + "&t=" + time + "&m=android&tag=" +
                   tag.ToText();
        }

        private static string _generateConfirmationHashForTime(string identitySecret, long time, string tag)
        {
            var decode = Convert.FromBase64String(identitySecret);
            var n2 = 8;
            if (tag != null)
            {
                if (tag.Length > 32)
                {
                    n2 = 8 + 32;
                }
                else
                {
                    n2 = 8 + tag.Length;
                }
            }
            var array = new byte[n2];
            var n3 = 8;
            while (true)
            {
                var n4 = n3 - 1;
                if (n3 <= 0)
                {
                    break;
                }
                array[n4] = (byte)time;
                time >>= 8;
                n3 = n4;
            }
            if (tag != null)
            {
                Array.Copy(Encoding.UTF8.GetBytes(tag), 0, array, 8, n2 - 8);
            }

            try
            {
                var hmacGenerator = new HMACSHA1 { Key = decode };
                var hashedData = hmacGenerator.ComputeHash(array);
                var encodedData = Convert.ToBase64String(hashedData);
                var hash = WebUtility.UrlEncode(encodedData);
                return hash;
            }
            catch (Exception)
            {
                return null; //Fix soon: catch-all is BAD!
            }
        }

        internal static List<IMobileConfirmation> CreateConfirmationList(string confirmationDataContent)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(confirmationDataContent);

            var mobileConfirmationList = document.QuerySelectorAll(".mobileconf_list_entry");
            List<IMobileConfirmation> mobileConfirmations = new List<IMobileConfirmation>();
            foreach (var mobileConfirmationDomElement in mobileConfirmationList)
            {
                var mobileConfirmation = Create(mobileConfirmationDomElement);
                mobileConfirmations.Add(mobileConfirmation);
            }
            return mobileConfirmations; 
        }
    }
}
