using skadisteam.trade.Constants;
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
            var errorCode =
                int.Parse(
                    steamErrorText.Replace(
                            RegexPatterns.SteamErrorFirstPart,
                            string.Empty)
                        .Replace(RegexPatterns.ClosedBracket, string.Empty));
            errEnum = (SteamError)errorCode;
            return errEnum;
        }
    }
}
