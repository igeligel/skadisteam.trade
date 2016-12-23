using System;

namespace skadisteam.trade.Interfaces
{
    public interface IMobileConfirmation
    {
        long Id { get; set; }
        ulong Key { get; set; }
        int Creator { get; set; }
        DateTime Time { get; set; }
    }
}
