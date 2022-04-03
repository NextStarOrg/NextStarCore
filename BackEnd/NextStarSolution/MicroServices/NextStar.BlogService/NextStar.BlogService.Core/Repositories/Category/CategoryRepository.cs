using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.Category;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{
    private readonly BlogDbContext _blogDbContext;

    public CategoryRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    
    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText)
    {
        var articles = await _blogDbContext.Categories.Where(x => x.Name.Contains(searchText ?? string.Empty)).AsNoTracking().OrderByDescending(x=>x.UpdatedTime).Select(x=> new CommonSingleOutput()
        {
            Id = x.Id,
            DisplayName = x.Name
        }).ToListAsync();
        return articles;
    }

    public IQueryable<BlogDbModels.Category> GetListQuery(Expression<Func<BlogDbModels.Category, bool>>? searchWhere)
    {
        if (searchWhere != null)
        {
            return _blogDbContext.Categories.Where(searchWhere).AsNoTracking();
        }

        return _blogDbContext.Categories.AsNoTracking();
    }

    public async Task AddEntityAsync(CategoryInput category)
    {
        var entityCategory = new BlogDbModels.Category()
        {
            Name = category.Name,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };
        await _blogDbContext.Categories.AddAsync(entityCategory);
        await _blogDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateEntityAsync(CategoryInput category)
    {
        var currentCategory = await _blogDbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        if (currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.UpdatedTime = DateTime.Now;
            _blogDbContext.Categories.Update(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteEntityAsync(int categoryId)
    {
        var currentCategory = await _blogDbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
        if (currentCategory != null)
        {
            _blogDbContext.Categories.Remove(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}