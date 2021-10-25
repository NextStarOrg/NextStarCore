using System;
using System.Linq;
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
using NextStar.Framework.Abstractions.Config;
using NextStar.Framework.AspNetCore.Extensions;
using NextStar.Framework.AspNetCore.Stores;
using NextStar.Framework.Core.Consts;
using NextStar.IdentityServer.Businesses;
using NextStar.IdentityServer.Dto;
using NextStar.IdentityServer.Filters;
using NextStar.IdentityServer.Models;

namespace NextStar.IdentityServer.Controllers
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonBusiness _commonBusiness;
        private readonly IAccountBusiness _business;
        private readonly INextStarApplicationConfig _nextStarApplicationConfig;
        private readonly INextStarSessionStore _nextStarSessionStore;

        public AccountController(ILogger<AccountController> logger,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            ICommonBusiness commonBusiness,
            IAccountBusiness business,
            INextStarApplicationConfig nextStarApplicationConfig,
            INextStarSessionStore nextStarSessionStore)
        {
            _logger = logger;
            _interaction = interaction;
            _clientStore = clientStore;
            _httpContextAccessor = httpContextAccessor;
            _commonBusiness = commonBusiness;
            _business = business;
            _nextStarApplicationConfig = nextStarApplicationConfig;
            _nextStarSessionStore = nextStarSessionStore;
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
        public async Task<IActionResult> Login(string returnUrl, LoginModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (ModelState.IsValid)
            {
                var userKey = await _business.LoginAsync(model);
                if (!userKey.HasValue)
                {
                    model.IsError = true;
                    ModelState.AddModelError("CheckOverall", "密码或账号错误");
                    return View(model);
                }
                model.IsError = false;
                

                var dto = new BuildUserSessionDto()
                {
                    UserKey = userKey.Value,
                    ClientId = context?.Client?.ClientName ?? "Unknown",
                    LoginProvider = NextStarLoginProvider.None,
                    Seconds = _nextStarApplicationConfig.GetConfigIntValue(NextStarApplicationName.CookieExpiredSeconds)
                };

                var auth = await _commonBusiness.BuildIdentityServerUserAsync(dto);
                return await PersonalSingInAsync(auth.User, context, auth.Props, model.ReturnUrl);
            }

            return View(model);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            try
            {
                // build a model so the logged out page knows what to display
                //var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);
                if (User != null)
                {
                    var sessionId = User.GetNextStarSessionId();
                    // delete local authentication cookie
                    await HttpContext.SignOutAsync();
                    try
                    {
                        if (sessionId != null)
                        {
                            await _nextStarSessionStore.DeleteAsync(sessionId.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Delete session {sessionId} occur error.", sessionId);
                    }
                }

                if (logoutId == null)
                {
                    await HttpContext.SignOutAsync();
                    //默认重定向到shcool site
                    var client = await _clientStore.FindClientByIdAsync(NextStarClientIds.ManageClientId);

                    return Redirect(client.PostLogoutRedirectUris.First());
                }
                else
                {
                    var context = await _interaction.GetLogoutContextAsync(logoutId);

                    if (context == null || string.IsNullOrEmpty(context.PostLogoutRedirectUri))
                    {
                        //默认重定向到parent site
                        var client = await _clientStore.FindClientByIdAsync(context?.ClientId ?? NextStarClientIds.ManageClientId);

                        return Redirect(client.PostLogoutRedirectUris.First());
                    }

                    return Redirect(context.PostLogoutRedirectUri);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Logout occur error.");

                await HttpContext.SignOutAsync();

                //默认重定向到parent site
                var client = await _clientStore.FindClientByIdAsync(NextStarClientIds.ManageClientId);
                return Redirect(client.PostLogoutRedirectUris.First());
            }
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