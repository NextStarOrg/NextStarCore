using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses.ArticleContent;
using NextStar.BlogService.Core.Entities.ArticleContent;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.API.Controllers.ArticleContent;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleContentController : ControllerBase
{
    private readonly IArticleContentBusiness _business;

    public ArticleContentController(IArticleContentBusiness business)
    {
        _business = business;
    }

    [HttpPost]
    public async Task<ICommonDto<Core.BlogDbModels.ArticleContent?>> GetContent(
        ArticleContentGetContentInput getContentInput)
    {
        var result = await _business.GetContentAsync(getContentInput);
        return CommonDto<Core.BlogDbModels.ArticleContent?>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<Core.BlogDbModels.ArticleContent>?>> GetList(
        ArticleContentSelectInput selectInput)
    {
        var result = await _business.GetListAsync(selectInput);
        return CommonDto<PageCommonDto<Core.BlogDbModels.ArticleContent>>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<bool>> Add(
        ArticleContentAddInput addInput)
    {
        await _business.AddAsync(addInput);
        return CommonDto<bool>.Ok(true);
    }
}