using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses;
using NextStar.BlogService.Core.Entities;
using NextStar.BlogService.Core.NextStarBlogDbModels;
using NextStar.Framework.Abstractions.Result;
using NextStar.Framework.AspNetCore.Result;
using NextStar.Framework.EntityFrameworkCore.Input;
using NextStar.Framework.EntityFrameworkCore.Output;

namespace NextStar.BlogService.API.Controllers;

[ApiController]
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

    /// <summary>
    /// 创建新作者
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ICommonDto<bool>> CreateAsync(AuthorCreatInput input)
    {
        try
        {
            var result = await _business.CreateAsync(input);
            return new CommonDto<bool>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Author create error: {0}",ex);
            return CommonDto<bool>.InternalServerErrorResult();
        }
    }
    
    /// <summary>
    /// 更新作者信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ICommonDto<bool>> UpdateAsync(AuthorUpdateInput input)
    {
        try
        {
            var result = await _business.UpdateAsync(input);
            return new CommonDto<bool>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Author create error: {0}",ex);
            return CommonDto<bool>.InternalServerErrorResult();
        }
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