namespace Facebook.Web.Tests.CanvasUrlBuilder.PrepareCanvasLoginUrlOAuthState.GivenNullReturnUrlPath.GivenNullCancelUrlPath.GivenNullState.GivenNullLoginParameters
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Facebook.Web;
    using Moq;
    using Xunit;

    public class GivenHttps_NoBeta_NoDefaultCancelUrlPathThen
    {
        private CanvasUrlBuilder _canvasUrlBuilder;

        private string _returnUrlPath;
        private string _cancelUrlPath;
        private string _sate;
        private IDictionary<string, object> _loginParameters;

        public GivenHttps_NoBeta_NoDefaultCancelUrlPathThen()
        {
            _canvasUrlBuilder = new CanvasUrlBuilder(
                new DefaultFacebookApplication
                {
                    SecureCanvasUrl = "https://localhost:16150/CSASPNETFacebookApp/",
                    CanvasPage = "http://apps.facebook.com/csharpsamplestwo/"
                },
                GetHttpRequest());
        }

        [Fact]
        public void ResultIsOfTypeJsonObject()
        {
            var result = _canvasUrlBuilder.PrepareCanvasLoginUrlOAuthState(
                _returnUrlPath, _cancelUrlPath, _sate, _loginParameters);

            Assert.IsType<JsonObject>(result);
        }

        [Fact]
        public void ResultContainsR()
        {
            var result = _canvasUrlBuilder.PrepareCanvasLoginUrlOAuthState(
                _returnUrlPath, _cancelUrlPath, _sate, _loginParameters);

            Assert.True(result.ContainsKey("r"));
        }

        [Fact]
        public void ResultDoesNotContainC()
        {
            var result = _canvasUrlBuilder.PrepareCanvasLoginUrlOAuthState(
               _returnUrlPath, _cancelUrlPath, _sate, _loginParameters);

            Assert.False(result.ContainsKey("c"));
        }

        [Fact]
        public void RIsSetCorrectly()
        {
            var result = _canvasUrlBuilder.PrepareCanvasLoginUrlOAuthState(
               _returnUrlPath, _cancelUrlPath, _sate, _loginParameters);

            Assert.Equal("https://apps.facebook.com/csharpsamplestwo/default.aspx", result["r"]);
        }

        [Fact]
        public void ResultDoesNotContainS()
        {
            var result = _canvasUrlBuilder.PrepareCanvasLoginUrlOAuthState(
               _returnUrlPath, _cancelUrlPath, _sate, _loginParameters);

            Assert.False(result.ContainsKey("s"));
        }

        [Fact]
        public void IsSecuredConnectionIsTrue()
        {
            Assert.True(_canvasUrlBuilder.IsSecureConnection);
        }

        [Fact]
        public void UseFacebookBetaIsFalse()
        {
            Assert.False(_canvasUrlBuilder.UseFacebookBeta);
        }

        public HttpRequestBase GetHttpRequest()
        {
            var requestMock = new Mock<HttpRequestBase>();

            requestMock.Setup(request => request.Url).Returns(new Uri("https://localhost:16150/CSASPNETFacebookApp/default.aspx"));
            requestMock.Setup(request => request.ApplicationPath).Returns("/CSASPNETFacebookApp");
            requestMock.Setup(request => request.RawUrl).Returns("/CSASPNETFacebookApp/");
            requestMock.Setup(request => request.UrlReferrer).Returns(new Uri("https://apps.facebook.com/csharpsamplestwo/"));

            return requestMock.Object;
        }
    }
}