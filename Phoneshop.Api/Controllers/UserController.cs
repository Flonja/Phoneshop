using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Api.DTO;
using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Phoneshop.Infrastructure.Identity;
using Microsoft.Extensions.Options;

namespace Phoneshop.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ErrorControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtTokenConfig _jwtTokenConfig;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, JwtTokenConfig jwtTokenConfig, IOptions<ApiBehaviorOptions> apiBehaviorOptions) : base(apiBehaviorOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenConfig = jwtTokenConfig;
        }

        // GET: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDTO model)
        {
            try
            {
                if(model.Password != model.ConfirmPassword)
                    return this.AddModelErrors($"{nameof(model.Password)} and {model.ConfirmPassword} are not equal.");

                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email, //model.FirstName.Replace(" ", "") + model.LastName.Replace(" ", ""),
                    Active = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return this.AddModelErrors(result);

                return await LoginUser(model);
            }
            catch(Exception ex)
            {
                return this.AddModelErrors(ex.ToString());
            }
        }

        // GET: api/user/token
        [HttpPost("token")]
        public async Task<IActionResult> LoginUser(UserAuthenticationDTO model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

                if (!result.Succeeded)
                    return this.AddModelErrors("User not found");

                var user = await _userManager.FindByEmailAsync(model.Email);

                if(!user.Active)
                    return this.AddModelErrors("User is blocked");

                var token = _jwtTokenConfig.GenerateToken(new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id) }, DateTime.Now);

                return Ok(new
                {
                    UserName = user.Email,
                    AccessToken = token
                });
            }
            catch (Exception ex)
            {
                return this.AddModelErrors(ex.ToString());
            }
        }
    }
}
