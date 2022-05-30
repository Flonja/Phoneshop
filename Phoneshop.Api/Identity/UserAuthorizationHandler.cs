using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Phoneshop.Domain.Entities;
using System.Threading.Tasks;

namespace Phoneshop.Api.Identity
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserTypeRequirement>
    {
        private readonly UserManager<User> _userManager;

        public UserAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user == null) return;

            if (user.Type >= requirement.UserType)
            {
                context.Succeed(requirement);
            }
        }
    }
}
