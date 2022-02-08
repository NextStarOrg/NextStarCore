using Microsoft.AspNetCore.Mvc;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.SystemService.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ThirdPartyLoginController : ControllerBase
{
    private readonly ILogger<ThirdPartyLoginController> _logger;

    public ThirdPartyLoginController(ILogger<ThirdPartyLoginController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ICommonDto<bool>> GetList()
    {
        return CommonDto<bool>.Ok(true);
    }
}