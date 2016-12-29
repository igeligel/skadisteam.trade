using System;
using AngleSharp.Parser.Html;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Confirmation;
using Xunit;

namespace skadisteam.trade.test.Extensions
{
    public class MobileConfirmationExtensionTest
    {
        private const string StandardImageUrl =
            "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/4f/4f8ab662e1e2478f9dcc9e64392f2ac232c06889.jpg";
        private const string StandardUsername = "Gaben";
        private readonly DateTime _defaultTime =
            DateTime.Parse("Friday, February 27, 2009 12:12:22 PM");
        private const int DefaultId = 1608621571;
        private const int DefaultCreator = 1752586421;
        private const long DefaultKey = 2676537912014354512;

        private readonly string[] _defaultItems =
        {
            "G3SG1 | Desert Storm",
            "XM1014 | Blue Spruce", "Fish", "(2) Moon"
        };

        [Fact]
        public void NullTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("in-game", StandardImageUrl, StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.NotNull(result);
        }

        [Fact]
        public void InGameOnlineStatusTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("in-game", StandardImageUrl, StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(OnlineStatus.InGame, result.PartnerOnlineStatus);
        }

        [Fact]
        public void OnlineStatusTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl, StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(OnlineStatus.Online, result.PartnerOnlineStatus);
        }

        [Fact]
        public void OfflineStatusTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("offline", StandardImageUrl, StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(OnlineStatus.Offline, result.PartnerOnlineStatus);
        }

        [Fact]
        public void PartnerImageTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online",
                    "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/4f/4f8ab662e1e2478f9dcc9e64392f2ac232c06889.jpg",
                    StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/4f/4f8ab662e1e2478f9dcc9e64392f2ac232c06889.jpg",
                result.PartnerImage);
        }

        [Fact]
        public void SecondPartnerImageTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online",
                    "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/ef/efca89264b868d38d5e0d30d142eb4fad6f873e5.jpg",
                    StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(
                "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/ef/efca89264b868d38d5e0d30d142eb4fad6f873e5.jpg",
                result.PartnerImage);
        }

        [Fact]
        public void PartnerNameTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl, "Gaben", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(
                "Gaben",
                result.PartnerName);
        }

        [Fact]
        public void SecondPartnerNameTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl, "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(
                "icefrog",
                result.PartnerName);
        }

        [Fact]
        public void FirstItemSetTradeConfirmationCheck()
        {
            var items = new[] { "Glove Case", "AWP | Asiimov" };
            var parser = new HtmlParser();
            var document = parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl, "icefrog", items));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(
                new[] { "Glove Case", "AWP | Asiimov" },
                result.Items);
        }

        [Fact]
        public void SecondItemSetTradeConfirmationCheck()
        {
            var items = new[] { "Glove Case" };
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", items));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(new[] {"Glove Case"}, result.Items);
        }

        [Fact]
        public void CreatorTradeConfirmationCheck()
        {
            const int creatorId = 1608621572;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, creatorId,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(1608621572, result.Creator);
        }

        [Fact]
        public void SecondCreatorTradeConfirmationCheck()
        {
            const int creatorId = 1608621412;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, creatorId,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(1608621412, result.Creator);
        }

        [Fact]
        public void IdTradeConfirmationCheck()
        {
            const int id = 1608621572;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(id, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(1608621572, result.Id);
        }

        [Fact]
        public void SecondIdTradeConfirmationCheck()
        {
            const int id = 1608621104;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(id, DefaultCreator,
                        DefaultKey, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(1608621104, result.Id);
        }

        [Fact]
        public void KeyTradeConfirmationCheck()
        {
            const ulong key = 2676537912014354325;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        key, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            // ulong comparison not allowed.
            Assert.Equal(2676537912014354325.ToString(), result.Key.ToString());
        }

        [Fact]
        public void SecondKeyTradeConfirmationCheck()
        {
            const ulong key = 2676537912014351476;
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    "icefrog", _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        key, _defaultTime)
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            // ulong comparison not allowed.
            Assert.Equal(2676537912014351476.ToString(), result.Key.ToString());
        }

        [Fact]
        public void TimeTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, DateTime.Parse("Friday, February 27, 2009 12:55:22 PM"))
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(DateTime.Parse("Friday, February 27, 2009 12:55:22 PM"), result.Time);
        }

        [Fact]
        public void SecondTimeTradeConfirmationCheck()
        {
            var parser = new HtmlParser();
            var document =
                parser.Parse(GetTestTradeConfirmation("online", StandardImageUrl,
                    StandardUsername, _defaultItems));
            var mobileConfirmationListEntry =
                document.QuerySelector(".mobileconf_list_entry");
            var result =
                GetDefaultMobileConfirmation(DefaultId, DefaultCreator,
                        DefaultKey, DateTime.Parse("Friday, February 27, 2009 5:55:22 PM"))
                    .ToTradeConfirmation(mobileConfirmationListEntry);
            Assert.Equal(DateTime.Parse("Friday, February 27, 2009 5:55:22 PM"), result.Time);
        }

        private static MobileConfirmation GetDefaultMobileConfirmation(int id, int creator, ulong key, DateTime date)
        {
            var mobileConfirmation = new MobileConfirmation
            {
                Id = id,
                Creator = creator,
                Key = key,
                Time = date
            };
            return mobileConfirmation;
        }

        private static string GetTestTradeConfirmation(string onlineStatus, string imageUrl, string username, string[] items)
        {
            const int confirmationId = 1608621571;
            const long key = 2676537912014354512;
            const int creator = 1752586421;
            const string mediumImageUrl =
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/4f/4f8ab662e1e2478f9dcc9e64392f2ac232c06889_medium.jpg";
            const string time = "Just now";

            return
                "<div class=\"mobileconf_list_entry\" id=\"conf" +
                confirmationId +
                "\" data-confid=\"" + confirmationId + "\" data-key=\"" + key +
                "\" data-type=\"2\" data-creator=\"" + creator +
                "\" data-cancel=\"Cancel\" data-accept=\"Accept\" > <div class=\"mobileconf_list_entry_content\"> <div class=\"mobileconf_list_entry_icon\"> <div class=\"playerAvatar " + onlineStatus + "\"><img src=\"" +
                imageUrl + "\" srcset=\"" + imageUrl + " 1x, " + mediumImageUrl +
                " 2x\"></div></div><div class=\"mobileconf_list_entry_description\"> <div>Trade with " +
                username +
                "</div><div>" + string.Join(", ", items) + "</div><div>" + time +
                "</div></div></div><div class=\"mobileconf_list_entry_sep\"></div></div>";
        }
    }
}
