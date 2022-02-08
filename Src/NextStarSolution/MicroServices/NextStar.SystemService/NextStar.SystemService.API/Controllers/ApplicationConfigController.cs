using Microsoft.AspNetCore.Mvc;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.SystemService.Core.Businesses.ApplicationConfig;
using NextStar.SystemService.Core.Entities.ApplicationConfig;
using NextStar.SystemService.Core.ManagementDbModels;

namespace NextStar.SystemService.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ApplicationConfigController : ControllerBase
{
    private readonly ILogger<ApplicationConfigController> _logger;
    private readonly IApplicationConfigBusiness _business;

    public ApplicationConfigController(ILogger<ApplicationConfigController> logger,
        IApplicationConfigBusiness business)
    {
        _logger = logger;
        _business = business;
    }

    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<ApplicationConfig>?>> GetList(SelectInput selectInput)
    {
        var result = await _business.GetApplicationConfigListAsync(selectInput);

        return CommonDto<PageCommonDto<ApplicationConfig>>.Ok(result);
    }
}