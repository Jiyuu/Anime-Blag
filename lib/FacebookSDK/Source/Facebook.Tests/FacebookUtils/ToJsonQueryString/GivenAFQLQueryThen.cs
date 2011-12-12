namespace Facebook.Tests.FacebookUtils.ToJsonQueryString
{
    using System.Dynamic;
    using Facebook;
    using Xunit;

    public class GivenAFQLQueryThen
    {
        [Fact]
        public void ShouldSerializeItCorrectly()
        {
            dynamic attachment = new ExpandoObject();
            attachment.name = "my attachment";
            attachment.href = "http://apps.facebook.com/canvas";

            dynamic parameters = new ExpandoObject();
            parameters.method = "stream.publish";
            parameters.message = "my message";
            parameters.attachment = attachment;

            string result = FacebookUtils.ToJsonQueryString(parameters);
            Assert.Equal("method=stream.publish&message=my+message&attachment=%7b%22name%22%3a%22my+attachment%22%2c%22href%22%3a%22http%3a%2f%2fapps.facebook.com%2fcanvas%22%7d", result);
        }
    }
}