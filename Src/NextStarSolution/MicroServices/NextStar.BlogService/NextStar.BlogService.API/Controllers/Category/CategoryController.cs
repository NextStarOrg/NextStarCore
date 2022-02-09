using Microsoft.AspNetCore.Mvc;

namespace NextStar.BlogService.API.Controllers.Category;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    public CategoryController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await Task.FromResult(Ok());
    }
}