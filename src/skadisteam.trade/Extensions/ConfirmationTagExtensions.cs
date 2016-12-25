using skadisteam.trade.Models.Confirmation;
using System.Collections.Generic;

namespace skadisteam.trade.Extensions
{
    internal static class ConfirmationTagExtensions
    {
        internal static Dictionary<ConfirmationTag, string> confirmationDictionary = new Dictionary<ConfirmationTag, string>()
        {
            { ConfirmationTag.Allow, "allow"},
            { ConfirmationTag.Confirm, "conf"}
        };

        internal static string ToText(this ConfirmationTag confirmationTag)
        {
            return confirmationDictionary[confirmationTag];
        }
    }
}
