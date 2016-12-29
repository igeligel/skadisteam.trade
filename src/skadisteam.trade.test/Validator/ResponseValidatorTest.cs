using System.Net;
using System.Net.Http;
using skadisteam.trade.Validator;
using Xunit;

namespace skadisteam.trade.test.Validator
{
    public class ResponseValidatorTest
    {
        [Fact]
        public void ValidResponseCheck()
        {
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
            var result =
                ResponseValidator.GetTradeOffersResponse(httpResponseMessage);
            Assert.True(result);
        }

        [Fact]
        public void InvalidResponseCheck()
        {
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden
            };
            var result =
                ResponseValidator.GetTradeOffersResponse(httpResponseMessage);
            Assert.False(result);
        }
    }
}
