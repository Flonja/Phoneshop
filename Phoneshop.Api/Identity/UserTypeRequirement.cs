using Microsoft.AspNetCore.Authorization;
using Phoneshop.Domain.Entities;

namespace Phoneshop.Api.Identity
{
    public class UserTypeRequirement : IAuthorizationRequirement
    {
        public UserTypeRequirement(UserType userType)
        {
            UserType = userType;
        }

        public UserType UserType { get; }
    }
}
