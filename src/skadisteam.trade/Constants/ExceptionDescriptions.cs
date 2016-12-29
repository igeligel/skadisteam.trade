namespace skadisteam.trade.Constants
{
    internal static class ExceptionDescriptions
    {
        internal const string DeviceIdAndIdentitySecretNotSet =
            "You have not set the device id and the identity secret which is required for this functionality. To fix this please use a constructor of this class where the device id and the identity secret is required as parameter.";

        internal const string DeviceIdNotSet =
            "You have not set your Device Id which is needed for this functionality. You should call the constructor of the client where you can set the device id.";

        internal const string IdentitySecretNotSet =
            "You have not set your identity secret which is needed for this functionality. You should call the constructor of the client where you can set the identity secret.";
    }
}
