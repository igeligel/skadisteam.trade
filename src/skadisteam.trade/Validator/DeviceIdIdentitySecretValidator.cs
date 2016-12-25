using skadisteam.trade.Constants;
using System;

namespace skadisteam.trade.Validator
{
    internal static class DeviceIdIdentitySecretValidator
    {
        internal static void Validate(string deviceId, string identitySecret)
        {
            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(identitySecret))
            {
                throw new ArgumentException(ExceptionDescriptions.DeviceIdAndIdentitySecretNotSet);
            }
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentException(ExceptionDescriptions.DeviceIdNotSet);
            }
            if (string.IsNullOrEmpty(identitySecret))
            {
                throw new ArgumentException(ExceptionDescriptions.IdentitySecretNotSet);
            }
        }
    }
}
