using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.Entities.CodeEnvironment;
using NextStar.BlogService.Core.Repositories.CodeEnvironment;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;

namespace NextStar.BlogService.Core.Businesses.CodeEnvironment;

public class CodeEnvironmentBusiness : ICodeEnvironmentBusiness
{
    private readonly ICodeEnvironmentRepository _repository;
    public CodeEnvironmentBusiness(ICodeEnvironmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText)
    {
        return await _repository.SearchSingleAsync(searchText);
    }
    public async Task<PageCommonDto<BlogDbModels.CodeEnvironment>> GetListAsync(CodeEnvironmentSelectInput selectInput)
    {
        var query = string.IsNullOrWhiteSpace(selectInput.SearchText)
            ? _repository.GetListQuery(null)
            : _repository.GetListQuery(x => x.Name.Contains(selectInput.SearchText));

        query = query.CommonPageSort(selectInput, "Id asc");

        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<BlogDbModels.CodeEnvironment>()
        {
            Data = result,
            TotalCount = count
        };
    }
    
    public async Task AddAsync(CodeEnvironmentInput codeEnvironment)
    {
        if (string.IsNullOrWhiteSpace(codeEnvironment.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.AddEntityAsync(codeEnvironment);
    }
    
    public async Task UpdateAsync(CodeEnvironmentInput codeEnvironment)
    {
        if (codeEnvironment.Key == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }
        if (string.IsNullOrWhiteSpace(codeEnvironment.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.UpdateEntityAsync(codeEnvironment);
    }

    public async Task DeleteAsync(Guid codeEnvironmentKey)
    {
        if (codeEnvironmentKey == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }

        await _repository.DeleteEntityAsync(codeEnvironmentKey);
    }
}