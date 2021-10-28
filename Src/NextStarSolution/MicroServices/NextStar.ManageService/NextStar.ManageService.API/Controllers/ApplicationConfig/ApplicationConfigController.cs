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
    [Route("api/[controller]")]
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

        [HttpGet("list")]
        public async Task<ICommonDto<List<ApplicationConfig>>> GetListAsync()
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
        
        [HttpGet("{id:int}")]
        public async Task<ICommonDto<ApplicationConfig>> GetDetailByIdAsync(int id)
        {
            try
            {
                var result = await _business.GetDetailByIdAsync(id);
                return new CommonDto<ApplicationConfig>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"application config get list error");
                return CommonDto<ApplicationConfig>.InternalServerErrorResult();
            }
        } 
        [HttpPut("{id:int}")]
        public async Task<ICommonDto<bool>> UpdateConfigAsync(int id,ApplicationConfig config)
        {
            try
            {
                if (id == config.Id)
                {
                    await _business.UpdateConfigAsync(config);
                    return new CommonDto<bool>(true);
                }
                return new CommonDto<bool>(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"application config get list error");
                return new CommonDto<bool>(false)
                {
                    ErrorCode = "500"
                };
            }
        } 
    }
}