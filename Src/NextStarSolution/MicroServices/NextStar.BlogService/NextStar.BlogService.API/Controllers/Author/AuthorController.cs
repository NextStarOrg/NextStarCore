using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses;
using NextStar.BlogService.Core.Entities;
using NextStar.BlogService.Core.NextStarBlogDbModels;
using NextStar.Framework.Abstractions.Result;
using NextStar.Framework.AspNetCore.Result;
using NextStar.Framework.EntityFrameworkCore.Output;

namespace NextStar.BlogService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuthorController:ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorBusiness _business;
    public AuthorController(ILogger<AuthorController> logger,
        IAuthorBusiness business)
    {
        _logger = logger;
        _business = business;
    }

    [HttpPost("list")]
    public async Task<ICommonDto<SelectListOutput<Author>>> GetListAsync(AuthorSelectInput input)
    {
        try
        {
            var result = await _business.GetListAsync(input);
            return new CommonDto<SelectListOutput<Author>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Author get list error: {0}",ex);
            return CommonDto<SelectListOutput<Author>>.InternalServerErrorResult();
        }
    }
}