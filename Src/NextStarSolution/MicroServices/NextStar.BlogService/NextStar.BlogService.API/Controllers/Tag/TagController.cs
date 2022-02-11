using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses.Tag;
using NextStar.BlogService.Core.Entities.Tag;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.API.Controllers.Tag;

[ApiController]
[Route("api/[controller]/[action]")]
public class TagController : ControllerBase
{
    private readonly ITagBusiness _business;
    public TagController(ITagBusiness business)
    {
        _business = business;
    }

    /// <summary>
    /// 获取标签列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ICommonDto<PageCommonDto<Core.BlogDbModels.Tag>?>> GetList(TagSelectInput selectInput)
    {
        var result = await _business.GetListAsync(selectInput);
        return CommonDto<PageCommonDto<Core.BlogDbModels.Tag>>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<bool>> Add(Core.BlogDbModels.Tag tag)
    {
        await _business.AddAsync(tag);
        return CommonDto<bool>.Ok(true);
    }

    [HttpPut]
    public async Task<ICommonDto<bool>> Update(Core.BlogDbModels.Tag tag)
    {
        await _business.UpdateAsync(tag);
        return CommonDto<bool>.Ok(true);
    }

    [HttpDelete("{tagKey:Guid}")]
    public async Task<ICommonDto<bool>> Delete(Guid tagKey)
    {
        await _business.DeleteAsync(tagKey);
        return CommonDto<bool>.Ok(true);
    }
}