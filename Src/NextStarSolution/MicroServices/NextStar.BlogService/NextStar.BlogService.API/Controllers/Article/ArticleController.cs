using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Entities.Article;

namespace NextStar.BlogService.API.Controllers.Article;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController:ControllerBase
{
    public ArticleController()
    {
        
    }

    [HttpPost]
    public async Task Add(ArticleInput articleInput)
    {
        
    }
}