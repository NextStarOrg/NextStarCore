using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.Category;

namespace NextStar.BlogService.Core.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{
    private readonly BlogDbContext _blogDbContext;

    public CategoryRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public IQueryable<BlogDbModels.Category> GetListQuery(Expression<Func<BlogDbModels.Category, bool>>? searchWhere)
    {
        if (searchWhere != null)
        {
            return _blogDbContext.Categories.Where(searchWhere).AsNoTracking();
        }

        return _blogDbContext.Categories.AsNoTracking();
    }

    public async Task AddEntityAsync(BlogDbModels.Category category)
    {
        var entityCategory = new BlogDbModels.Category()
        {
            Key = Guid.NewGuid(),
            Name = category.Name,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };
        await _blogDbContext.Categories.AddAsync(entityCategory);
        await _blogDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateEntityAsync(BlogDbModels.Category category)
    {
        var currentCategory = await _blogDbContext.Categories.FirstOrDefaultAsync(x => x.Key == category.Key);
        if (currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.UpdatedTime = DateTime.Now;
            _blogDbContext.Categories.Update(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteEntityAsync(Guid categoryKey)
    {
        var currentCategory = await _blogDbContext.Categories.FirstOrDefaultAsync(x => x.Key == categoryKey);
        if (currentCategory != null)
        {
            _blogDbContext.Categories.Remove(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}