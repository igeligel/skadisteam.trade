using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using skadisteam.trade.Factories;
using skadisteam.trade.Models;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class OnlineStatusFactoryTest
    {
        private const string HtmlBase =
            "<div class=\"parent\">" + "<div class=\"playerAvatar ";

        private const string HtmlAvatar =
            "\"><img src=\"http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/c5/c5d56249ee5d28a07db4ac9f7f60af961fab5426.jpg\" srcset=\"http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/c5/c5d56249ee5d28a07db4ac9f7f60af961fab5426.jpg 1x, http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/c5/c5d56249ee5d28a07db4ac9f7f60af961fab5426_medium.jpg 2x\"></div></div>";
        private const string GabenProfileOffline =
            HtmlBase + "offline" + HtmlAvatar;
        private const string GabenProfileOnline =
            HtmlBase + "online" + HtmlAvatar;
        private const string GabenProfileInGame =
            HtmlBase + "in-game" + HtmlAvatar;

        [Fact]
        public void OfflineCheck()
        {
            var nodeElement = CreateElement(GabenProfileOffline);
            var onlineStatus = OnlineStatusFactory.Create(nodeElement);
            Assert.Equal(OnlineStatus.Offline, onlineStatus);
        }

        [Fact]
        public void OnlineCheck()
        {
            var nodeElement = CreateElement(GabenProfileOnline);
            var onlineStatus = OnlineStatusFactory.Create(nodeElement);
            Assert.Equal(OnlineStatus.Online, onlineStatus);
        }

        [Fact]
        public void InGameCheck()
        {
            var nodeElement = CreateElement(GabenProfileInGame);
            var onlineStatus = OnlineStatusFactory.Create(nodeElement);
            Assert.Equal(OnlineStatus.InGame, onlineStatus);
        }

        private static IParentNode CreateElement(string input)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(input);
            return document.QuerySelector(".parent");
        }
    }
}
