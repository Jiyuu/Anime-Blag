namespace Facebook.Web.Tests.FacebookSignedRequest.TryParse.internal_method
{
    using System;
    using Facebook;
    using Xunit;

    public class GivenASignedRequestWhoseValidityIsForOneHourAndIsOlderThanOneHourThen
    {
        [Fact]
        public void ShouldThrowInvalidOperationException()
        {
            var signedRequest = "t63pZQ4Q3ZTHJt0hOsKrY2pb28xRlduW0pg4lL_Zhl4.eyJhbGdvcml0aG0iOiJBRVMtMjU2LUNCQyBITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTI4NzYwMTk4OCwiaXYiOiJmRExKQ1cteWlYbXVOYTI0ZVNhckpnIiwicGF5bG9hZCI6IllHeW00cG9Rbk1UckVnaUFPa0ZUVkk4NWxsNVJ1VWlFbC1JZ3FmeFRPVEhRTkl2VlZJOFk4a1Z1T29lS2FXT2Vhc3NXRlRFdjBRZ183d0NDQkVlbjdsVUJCemxGSjFWNjNISjNBZjBTSW5nY3hXVEo3TDZZTGF0TW13WGdEQXZXbjVQc2ZxeldrNG1sOWg5RExuWXB0V0htREdMNmlCaU9oTjdXeUk3cDZvRXBWcmlGdUp3X2NoTG9QYjhhM3ZHRG5vVzhlMlN4eDA2QTJ4MnhraWFwdmcifQ";

            string secret = "13750c9911fec5865d01f3bd00bdf4db";
            int maxAge = 3600;
            double currentTime = 1294741460;

            Assert.Throws<InvalidOperationException>(() => FacebookSignedRequest.TryParse(secret, signedRequest, maxAge, currentTime, true));
        }

        [Fact]
        public void ErrorMessageShouldBeInvalidSingedRequest()
        {
            var signedRequest = "t63pZQ4Q3ZTHJt0hOsKrY2pb28xRlduW0pg4lL_Zhl4.eyJhbGdvcml0aG0iOiJBRVMtMjU2LUNCQyBITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTI4NzYwMTk4OCwiaXYiOiJmRExKQ1cteWlYbXVOYTI0ZVNhckpnIiwicGF5bG9hZCI6IllHeW00cG9Rbk1UckVnaUFPa0ZUVkk4NWxsNVJ1VWlFbC1JZ3FmeFRPVEhRTkl2VlZJOFk4a1Z1T29lS2FXT2Vhc3NXRlRFdjBRZ183d0NDQkVlbjdsVUJCemxGSjFWNjNISjNBZjBTSW5nY3hXVEo3TDZZTGF0TW13WGdEQXZXbjVQc2ZxeldrNG1sOWg5RExuWXB0V0htREdMNmlCaU9oTjdXeUk3cDZvRXBWcmlGdUp3X2NoTG9QYjhhM3ZHRG5vVzhlMlN4eDA2QTJ4MnhraWFwdmcifQ";

            string secret = "13750c9911fec5865d01f3bd00bdf4db";
            int maxAge = 3600;
            double currentTime = 1294741460;

            Exception exception = null;
            try
            {
                FacebookSignedRequest.TryParse(secret, signedRequest, maxAge, currentTime, true);
            }
            catch (InvalidOperationException ex)
            {
                exception = ex;
            }

            Assert.Equal(Properties.Resources.OldSignedRequest, exception.Message);
        }
    }
}