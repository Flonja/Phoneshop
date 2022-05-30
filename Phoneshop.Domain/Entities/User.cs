using Microsoft.AspNetCore.Identity;

namespace Phoneshop.Domain.Entities
{
    public class User : IdentityUser
    {
        public UserType Type { get; set; } = UserType.Customer;
        public bool Active { get; set; } = false;
    }
}
