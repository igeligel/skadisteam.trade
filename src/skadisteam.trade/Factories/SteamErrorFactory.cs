using skadisteam.trade.Models;
using skadisteam.trade.Models.Json.AcceptingOffers;

namespace skadisteam.trade.Factories
{
    internal static class SteamErrorFactory
    {
        internal static SteamError ParseError(SteamErrorResponse steamErrorResponse)
        {
            return ParseError(steamErrorResponse.StrError);
        }

        internal static SteamError ParseError(string steamErrorText)
        {
            var errEnum = SteamError.Undefined;
            if (steamErrorText == null) return errEnum;
            var number = int.Parse(steamErrorText.Split('(', ')')[1]);
            errEnum = (SteamError)number;
            return errEnum;
        }
    }
}
