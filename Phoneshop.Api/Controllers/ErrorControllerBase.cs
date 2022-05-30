using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Phoneshop.Api.Controllers
{
    public class ErrorControllerBase : ControllerBase
    {
        private readonly ApiBehaviorOptions _apiBehaviorOptions;

        public ErrorControllerBase(IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            _apiBehaviorOptions = apiBehaviorOptions.Value;
        }

        public IActionResult AddModelErrors(string error)
        {
            return AddModelErrors(new List<string>() { error });
        }

        public IActionResult AddModelErrors(IdentityResult result)
        {
            return AddModelErrors(result.Errors.Select(error => error.Description));
        }

        public IActionResult AddModelErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            return _apiBehaviorOptions.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
