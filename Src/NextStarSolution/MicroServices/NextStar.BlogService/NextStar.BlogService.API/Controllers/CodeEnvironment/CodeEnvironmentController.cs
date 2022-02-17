using Microsoft.AspNetCore.Mvc;
using NextStar.BlogService.Core.Businesses.CodeEnvironment;
using NextStar.BlogService.Core.Entities.CodeEnvironment;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.API.Controllers.CodeEnvironment;

[ApiController]
[Route("api/[controller]/[action]")]
public class CodeEnvironmentController
{
    private readonly ICodeEnvironmentBusiness _business;

    public CodeEnvironmentController(ICodeEnvironmentBusiness business)
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
    public async Task<ICommonDto<PageCommonDto<Core.BlogDbModels.CodeEnvironment>?>> GetList(
        CodeEnvironmentSelectInput selectInput)
    {
        var result = await _business.GetListAsync(selectInput);
        return CommonDto<PageCommonDto<Core.BlogDbModels.CodeEnvironment>>.Ok(result);
    }

    [HttpPost]
    public async Task<ICommonDto<bool>> Add(Core.BlogDbModels.CodeEnvironment codeEnvironment)
    {
        await _business.AddAsync(codeEnvironment);
        return CommonDto<bool>.Ok(true);
    }

    [HttpPut]
    public async Task<ICommonDto<bool>> Update(Core.BlogDbModels.CodeEnvironment codeEnvironment)
    {
        await _business.UpdateAsync(codeEnvironment);
        return CommonDto<bool>.Ok(true);
    }

    [HttpDelete("{codeEnvironmentKey:Guid}")]
    public async Task<ICommonDto<bool>> Delete(Guid codeEnvironmentKey)
    {
        await _business.DeleteAsync(codeEnvironmentKey);
        return CommonDto<bool>.Ok(true);
    }
}