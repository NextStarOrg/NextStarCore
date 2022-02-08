﻿using Microsoft.AspNetCore.Mvc;
using NextStar.Library.MicroService.Outputs;
using NextStar.SystemService.Core.Entities.ThirdPartyLogin;

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
    public async Task<ICommonDto<bool>> GetList(ThirdPartyLoginSelectInput selectInput)
    {
        return CommonDto<bool>.Ok(true);
    }
}