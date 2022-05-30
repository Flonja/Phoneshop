using Phoneshop.Domain.Abstractions;
using Phoneshop.Domain.Entities;
using Phoneshop.Infrastructure.Extensions;
using PhoneshopNuget.Repository;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phoneshop.Infrastructure.Implementations
{
    public class PhoneEndpoints : Endpoints<Phone>
    {
        public PhoneEndpoints(IHttpSession session)
        {
            string phoneUri = "phone";
            GetAllUri = _ => Task.FromResult(phoneUri.AsSimpleHttpRequest(HttpMethod.Get));
            GetOneUri = phone => Task.FromResult($"{phoneUri}/{phone.Id}".AsSimpleHttpRequest(HttpMethod.Get));
            AddUri = async phone =>
            {
                string token = await session.GetToken();
                return phoneUri.AsBodyHttpRequest(HttpMethod.Post, phone, token);
            };
            UpdateUri = async phone =>
            {
                string token = await session.GetToken();
                return phoneUri.AsBodyHttpRequest(HttpMethod.Put, phone, token);
            };
            RemoveUri = async phone =>
            {
                string token = await session.GetToken();
                return $"{phoneUri}?id={phone.Id}".AsSimpleHttpRequest(HttpMethod.Delete, token);
            };
        }
    }
}
