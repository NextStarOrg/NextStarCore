using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.Entities.Category;
using NextStar.BlogService.Core.Repositories.Category;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;

namespace NextStar.BlogService.Core.Businesses.Category;

public class CategoryBusiness:ICategoryBusiness
{
    private readonly ICategoryRepository _repository;
    public CategoryBusiness(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageCommonDto<BlogDbModels.Category>> GetListAsync(CategorySelectInput selectInput)
    {
        var query = string.IsNullOrWhiteSpace(selectInput.SearchText)
            ? _repository.GetListQuery(null)
            : _repository.GetListQuery(x => x.Name.Contains(selectInput.SearchText));

        query = query.CommonPageSort(selectInput, "Id asc");

        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<BlogDbModels.Category>()
        {
            Data = result,
            TotalCount = count
        };
    }
    
    public async Task AddAsync(BlogDbModels.Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.AddEntityAsync(category);
    }
    
    public async Task UpdateAsync(BlogDbModels.Category category)
    {
        if (category.Key == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }
        if (string.IsNullOrWhiteSpace(category.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.UpdateEntityAsync(category);
    }

    public async Task DeleteAsync(Guid categoryKey)
    {
        if (categoryKey == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }

        await _repository.DeleteEntityAsync(categoryKey);
    }
}