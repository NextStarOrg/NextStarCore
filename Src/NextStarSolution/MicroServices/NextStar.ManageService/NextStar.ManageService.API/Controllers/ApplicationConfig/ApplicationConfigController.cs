using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Result;
using NextStar.Framework.AspNetCore.Result;
using NextStar.ManageService.Core.Businesses;
using NextStar.ManageService.Core.NextStarAccountDbModels;

namespace NextStar.ManageService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public async Task<ICommonDto<List<ApplicationConfig>>> GetList()
        {
            try
            {
                var result = await _business.GetListAsync();
                return new CommonDto<List<ApplicationConfig>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"application config get list error");
                return CommonDto<List<ApplicationConfig>>.InternalServerErrorResult();
            }
        }
        
    }
}