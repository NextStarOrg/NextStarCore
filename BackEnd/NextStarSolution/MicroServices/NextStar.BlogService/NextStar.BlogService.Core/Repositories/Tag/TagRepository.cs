using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.Tag;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.Tag;

public class TagRepository : ITagRepository
{
    private readonly BlogDbContext _blogDbContext;

    public TagRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    
    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText)
    {
        var articles = await _blogDbContext.Tags.Where(x => x.Name.Contains(searchText ?? string.Empty)).AsNoTracking().OrderByDescending(x=>x.UpdatedTime).Select(x=> new CommonSingleOutput()
        {
            Key = x.Key,
            DisplayName = x.Name
        }).ToListAsync();
        return articles;
    }

    public IQueryable<BlogDbModels.Tag> GetListQuery(Expression<Func<BlogDbModels.Tag, bool>>? searchWhere)
    {
        if (searchWhere != null)
        {
            return _blogDbContext.Tags.Where(searchWhere).AsNoTracking();
        }

        return _blogDbContext.Tags.AsNoTracking();
    }

    public async Task AddEntityAsync(TagInput tag)
    {
        var newTag = new BlogDbModels.Tag()
        {
            Key = Guid.NewGuid(),
            Name = tag.Name,
            BackgroundColor = string.IsNullOrWhiteSpace(tag.BackgroundColor) ? null : tag.BackgroundColor,
            TextColor = string.IsNullOrWhiteSpace(tag.TextColor) ? null : tag.TextColor,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };
        await _blogDbContext.Tags.AddAsync(newTag);
        await _blogDbContext.SaveChangesAsync();
    }

    public async Task UpdateEntityAsync(TagInput tag)
    {
        var currentEntity = await _blogDbContext.Tags.FirstOrDefaultAsync(x => x.Key == tag.Key);
        if (currentEntity != null)
        {
            currentEntity.Name = tag.Name;
            currentEntity.UpdatedTime = DateTime.Now;
            currentEntity.BackgroundColor = string.IsNullOrWhiteSpace(tag.BackgroundColor) ? currentEntity.BackgroundColor : tag.BackgroundColor;
            currentEntity.TextColor = string.IsNullOrWhiteSpace(tag.TextColor) ? currentEntity.TextColor : tag.TextColor;
            _blogDbContext.Tags.Update(currentEntity);
            await _blogDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteEntityAsync(Guid tagKey)
    {
        var currentCategory = await _blogDbContext.Tags.FirstOrDefaultAsync(x => x.Key == tagKey);
        if (currentCategory != null)
        {
            _blogDbContext.Tags.Remove(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}