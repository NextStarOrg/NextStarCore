using System.Text.Json;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NextStar.Identity.Businesses;
using NextStar.Identity.Entities;
using NextStar.Identity.Models;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.Core.Consts;

namespace NextStar.Identity.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IThirdPartyLogin _thirdPartyLogin;
    private readonly IAccountBusiness _business;
    private readonly IApplicationConfigStore _applicationConfigStore;

    public AccountController(ILogger<AccountController> logger,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IThirdPartyLogin thirdPartyLogin,
        IAccountBusiness business,
        IApplicationConfigStore applicationConfigStore)
    {
        _logger = logger;
        _interaction = interaction;
        _clientStore = clientStore;
        _thirdPartyLogin = thirdPartyLogin;
        _business = business;
        _applicationConfigStore = applicationConfigStore;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        if (!await _business.ValidateUserIsAuthenticated(User))
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
        var isAllowPasswordLogin = await
            _applicationConfigStore.GetConfigBoolAsync(NextStarApplicationName.IsAllowPasswordLogin);
        if (isAllowPasswordLogin)
        {
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> ThirdPartyLogin(string routingParameters)
    {
        var isEnum = Enum.TryParse<NextStarLoginType>(routingParameters, true, out var provider);
        if (isEnum)
        {
            try
            {
                var url = await _thirdPartyLogin.GetAuthorizationUrlAsync(string.Empty, provider);
                return Redirect(url);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{RoutingParameters} is get openid configuration key error", routingParameters);
                return BadRequest();
            }
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> ThirdPartyCallback(string routingParameters, string state, string code)
    {
        var isEnum = Enum.TryParse<NextStarLoginType>(routingParameters, true, out var provider);
        if (isEnum)
        {
            try
            {
                var loginInfo = await _thirdPartyLogin.PostRequestTokenAsync(state, code, provider);
                var userKey = await _business.ThirdPartyLogin(loginInfo);
                // 如果返回后查询没有关联其他数据则返回对于Key并提示用户添加进去
                if (userKey == null) return View(loginInfo);

                var context = await _interaction.GetAuthorizationContextAsync(loginInfo.ReturnUrl);
                var buildUserSessionDto = new BuildUserSessionDto()
                {
                    UserKey = userKey.Value,
                    Provider = loginInfo.Provider,
                    ClientId = "Unknown",
                    ThirdPartyEmail = loginInfo.Email,
                    ThirdPartyName = loginInfo.Name,
                    Seconds = await _applicationConfigStore.GetConfigIntAsync(NextStarApplicationName
                        .CookieExpiredSeconds)
                };
                if (context != null)
                {
                    buildUserSessionDto.ClientId = context.Client.ClientId;
                    buildUserSessionDto.Seconds = context.Client.AccessTokenLifetime;
                }

                var identityServerUser = await _business.BuildIdentityServerUserAsync(buildUserSessionDto);
                if (identityServerUser == null) return BadRequest();

                var props = await _business.GetAuthProp();
                return await PersonalSingInAsync(identityServerUser, context, props, loginInfo.ReturnUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{RoutingParameters} is get key error", routingParameters);
                return BadRequest();
            }
        }

        return BadRequest();
    }

    #region Private Method

    private async Task<IActionResult> PersonalSingInAsync(IdentityServerUser user,
        AuthorizationRequest? context,
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

        if (string.IsNullOrEmpty(returnUrl))
        {
            return Redirect("~/");
        }

        // user might have clicked on a malicious link - should be logged
        _logger.LogError("invalid return URL: {ReturnUrl}", returnUrl);
        return StatusCode(500);
    }

    #endregion
}