using System.Text.Json;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NextStar.Identity.Businesses;
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
    private readonly ICommonBusiness _commonBusiness;
    private readonly IApplicationConfigStore _applicationConfigStore;

    public AccountController(ILogger<AccountController> logger,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IThirdPartyLogin thirdPartyLogin,
        ICommonBusiness commonBusiness,
        IApplicationConfigStore applicationConfigStore)
    {
        _logger = logger;
        _interaction = interaction;
        _clientStore = clientStore;
        _thirdPartyLogin = thirdPartyLogin;
        _commonBusiness = commonBusiness;
        _applicationConfigStore = applicationConfigStore;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        if (!await _commonBusiness.ValidateUserIsAuthenticated(User))
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
        var isAllowPasswordLogin =await
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
            var url = await _thirdPartyLogin.GetAuthorizationUrlAsync(string.Empty, provider);
            return Redirect(url);
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> ThirdPartyCallback(string routingParameters, string state, string code)
    {
        var isEnum = Enum.TryParse<NextStarLoginType>(routingParameters, true, out var provider);
        if (isEnum)
        {
            var loginInfo = await _thirdPartyLogin.PostRequestTokenAsync(state, code, provider);
            //TODO: 如果返回后查询没有关联其他数据则返回对于Key并提示用户添加进去
            //return Ok(JsonSerializer.Serialize(url));
            return View(loginInfo);
        }

        return BadRequest();
    }
}