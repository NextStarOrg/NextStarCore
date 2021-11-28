using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NextStar.Identity.Models;

namespace NextStar.Identity.Controllers;

public class AccountController:Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;

    public AccountController(ILogger<AccountController> logger,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore)
    {
        _logger = logger;
        _interaction = interaction;
        _clientStore = clientStore;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        // if (!await _commonBusiness.ValidateUserAuthenticated(User))
        // {
        //     await HttpContext.SignOutAsync();
        // }

        var model = new LoginModel()
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }
}