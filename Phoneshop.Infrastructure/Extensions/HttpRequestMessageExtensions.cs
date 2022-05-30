using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Phoneshop.Infrastructure.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AsSimpleHttpRequest(this string uri, HttpMethod method, string token = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return request;
        }

        public static HttpRequestMessage AsBodyHttpRequest(this string uri, HttpMethod method, string body, string token = null)
        {
            var message = AsSimpleHttpRequest(uri, method, token);
            message.Content = new StringContent(body);

            return message;
        }

        public static HttpRequestMessage AsBodyHttpRequest(this string uri, HttpMethod method, object body, string token = null)
        {
            return AsBodyHttpRequest(uri, method, JsonSerializer.Serialize(body), token);
        }
    }
}
