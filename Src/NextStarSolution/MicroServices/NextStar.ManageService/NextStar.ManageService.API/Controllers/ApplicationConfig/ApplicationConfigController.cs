using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Result;
using NextStar.Framework.AspNetCore.Result;

namespace NextStar.ManageService.API.Controllers.ApplicationConfig
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ApplicationConfigController : ControllerBase
    {
        private readonly ILogger<ApplicationConfigController> _logger;
        public ApplicationConfigController(ILogger<ApplicationConfigController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ICommonDto<string>> GetList()
        {
            return await Task.FromResult(CommonDto<string>.OkResult());
        }
        
    }
}