using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses.Category;
using NextStar.BlogService.Core.Entities.Category;
using NextStar.Library.AspNetCore.ApplicationDbModels;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.API.Controllers.Category;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryBusiness _business;

    public CategoryController(ICategoryBusiness business)
    {
        _business = business;
    }

    [HttpGet]
    public async Task<ICommonDto<List<CommonSingleOutput>?>> GetSingle(string? searchText)
    {
        var result = await _business.SearchSingleAsync(searchText);
        return CommonDto<List<CommonSingleOutput>?>.Ok(result);
    }
    /// <summary>
    /// 获取分类列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<Core.BlogDbModels.Category>?>> GetList(CategorySelectInput selectInput)
    {
        var result = await _business.GetListAsync(selectInput);
        return CommonDto<PageCommonDto<Core.BlogDbModels.Category>>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<bool>> Add(CategoryInput category)
    {
        await _business.AddAsync(category);
        return CommonDto<bool>.Ok(true);
    }

    [HttpPut]
    public async Task<ICommonDto<bool>> Update(CategoryInput category)
    {
        await _business.UpdateAsync(category);
        return CommonDto<bool>.Ok(true);
    }

    [HttpDelete("{categoryKey:int}")]
    public async Task<ICommonDto<bool>> Delete(int categoryId)
    {
        await _business.DeleteAsync(categoryId);
        return CommonDto<bool>.Ok(true);
    }
}