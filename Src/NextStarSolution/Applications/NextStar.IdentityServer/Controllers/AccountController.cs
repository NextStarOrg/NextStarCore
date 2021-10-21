using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NextStar.IdentityServer.Controllers
{
    public class AccountController:Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
    }
}