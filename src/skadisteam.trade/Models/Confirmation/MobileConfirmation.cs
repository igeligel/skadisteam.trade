using System;
using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.Confirmation
{
    public class MobileConfirmation: IMobileConfirmation
    {
        public long Id { get; set; }
        public ulong Key { get; set; }
        public long Creator { get; set; }
        public DateTime Time { get; set; }
    }
}
