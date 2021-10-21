using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextStar.IdentityServer.Businesses;
using NextStar.IdentityServer.Filters;
using NextStar.IdentityServer.Models.Account;

namespace NextStar.IdentityServer.Controllers
{
    [SecurityHeaders]
    public class AccountController:Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonBusiness _commonBusiness;
        public AccountController(ILogger<AccountController> logger,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            ICommonBusiness commonBusiness)
        {
            _logger = logger;
            _interaction = interaction;
            _clientStore = clientStore;
            _httpContextAccessor = httpContextAccessor;
            _commonBusiness = commonBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (!await _commonBusiness.ValidateUserAuthenticated(User))
            {
                await HttpContext.SignOutAsync();
            }

            var model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }

            return BadRequest();
        }
        
        private async Task<IActionResult> PersonalSingInAsync(IdentityServerUser user,
            AuthorizationRequest context,
            AuthenticationProperties props,
            string returnUrl)
        {
            await HttpContext.SignInAsync(user, props);

            if (context != null)
            {
                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                return Redirect(returnUrl);
            }

            // request for a local page
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("~/");
            }
            else
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
        }
    }
}