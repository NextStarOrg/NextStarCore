using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses.Article;
using NextStar.BlogService.Core.Entities.Article;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.API.Controllers.Article;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController:ControllerBase
{
    private readonly IArticleBusiness _business;
    public ArticleController(IArticleBusiness business)
    {
        _business = business;
    }

    [HttpGet]
    public async Task<ICommonDto<List<CommonSingleOutput>?>> GetSingle(string? searchText)
    {
        var result = await _business.SearchSingleAsync(searchText);
        return CommonDto<List<CommonSingleOutput>?>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<ArticleItem>?>> GetList(ArticleSelectInput selectInput)
    {
        var result = await _business.SelectArticleAsync(selectInput);
        return CommonDto<PageCommonDto<ArticleItem>?>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<bool>> Add(ArticleInput articleInput)
    {
        await _business.AddAsync(articleInput);
        return CommonDto<bool>.Ok(true);
    }
    
    [HttpPut]
    public async Task<ICommonDto<bool>> Update(ArticleInput articleInput)
    {
        await _business.UpdateAsync(articleInput);
        return CommonDto<bool>.Ok(true);
    }
    
    [HttpDelete("{articleKey:guid}")]
    public async Task<ICommonDto<bool>> Update(Guid articleKey)
    {
        await _business.DeleteAsync(articleKey);
        return CommonDto<bool>.Ok(true);
    }
}