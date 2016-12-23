using System;

namespace skadisteam.trade.Exceptions
{
    public class OfferNotValidException : Exception
    {
        public OfferNotValidException()
        {

        }

        public OfferNotValidException(string message)
        : base(message)
        {
        }

        public OfferNotValidException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
