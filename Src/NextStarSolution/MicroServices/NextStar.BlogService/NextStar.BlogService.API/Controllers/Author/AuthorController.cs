using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NextStar.BlogService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuthorController:ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    public AuthorController(ILogger<AuthorController> logger)
    {
        _logger = logger;
    }
}