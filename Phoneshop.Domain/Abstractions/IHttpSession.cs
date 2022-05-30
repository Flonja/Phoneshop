using Phoneshop.Domain.Models;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Abstractions
{
    public interface IHttpSession
    {
        public Task<string> GetToken();
        public Task Login(LoginInputModel model);
        public Task Register(LoginInputModel model);
        public Task Logout();
    }
}
