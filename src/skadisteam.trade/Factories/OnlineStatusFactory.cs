using System.Linq;
using AngleSharp.Dom;
using skadisteam.trade.Constants;
using skadisteam.trade.Models;

namespace skadisteam.trade.Factories
{
    internal static class OnlineStatusFactory
    {
        internal static OnlineStatus Create(IParentNode angleSharpElement)
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
    }
}
