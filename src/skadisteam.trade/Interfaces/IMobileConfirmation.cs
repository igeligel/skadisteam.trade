using System;

namespace skadisteam.trade.Interfaces
{
    public interface IMobileConfirmation
    {
        long Id { get; set; }
        ulong Key { get; set; }
        long Creator { get; set; }
        DateTime Time { get; set; }
    }
}
