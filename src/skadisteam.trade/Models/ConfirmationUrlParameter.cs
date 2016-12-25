using skadisteam.trade.Interfaces;
using skadisteam.trade.Models.Confirmation;
namespace skadisteam.trade.Models
{
    internal class ConfirmationUrlParameter
    {
        internal ConfirmationTag ConfirmationTag { get; set; }
        internal string DeviceId { get; set; }
        internal string IdentitySecret { get; set; }
        internal long SteamCommunityId { get; set; }
        internal IMobileConfirmation MobileConfirmation { get; set; } 
    }
}
