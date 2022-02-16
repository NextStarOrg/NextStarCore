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

    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<ArticleItem>?>> GetList(ArticleSelectInput selectInput)
    {
        var result = await _business.SelectArticleAsync(selectInput);
        return CommonDto<PageCommonDto<ArticleItem>?>.Ok(result);
    }
}