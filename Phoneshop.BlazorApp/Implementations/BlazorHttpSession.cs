using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json.Linq;
using Phoneshop.Domain.Abstractions;
using Phoneshop.Domain.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Phoneshop.BlazorApp.Implementations
{
    public class BlazorHttpSession : IHttpSession
    {
        public BlazorHttpSession(ProtectedSessionStorage protectedSessionStore, HttpClient client)
        {
            _protectedSessionStore = protectedSessionStore;
            _client = client;
        }

        private readonly ProtectedBrowserStorage _protectedSessionStore;
        private readonly HttpClient _client;

        protected async Task SendRequest(LoginInputModel model, string uri)
        {
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            using var response = await _client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

            var value = await JsonSerializer.DeserializeAsync<LoginOutputModel>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
            await SetToken(value.AccessToken);
        }

        public async Task Login(LoginInputModel model)
        {
            await SendRequest(model, "user/token");
        }

        public async Task Register(LoginInputModel model)
        {
            await SendRequest(model, "user");
        }

        public async Task<string> GetToken()
        {
            var result = await _protectedSessionStore.GetAsync<string>("token");
            return result.Success ? result.Value : null;
        }

        protected async Task SetToken(string token)
        {
            await _protectedSessionStore.SetAsync("token", token);
        }

        public async Task Logout()
        {
            await _protectedSessionStore.DeleteAsync("token");
        }
    }

    public class LoginOutputModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
