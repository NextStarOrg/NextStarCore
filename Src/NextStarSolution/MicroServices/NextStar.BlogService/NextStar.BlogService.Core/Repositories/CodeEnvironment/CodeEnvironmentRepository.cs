using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.DbContexts;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.CodeEnvironment;

public class CodeEnvironmentRepository : ICodeEnvironmentRepository
{
    private readonly BlogDbContext _blogDbContext;

    public CodeEnvironmentRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string searchText)
    {
        var articles = await _blogDbContext.CodeEnvironments.Where(x => x.Name.Contains(searchText)).AsNoTracking().OrderByDescending(x=>x.UpdatedTime).Select(x=> new CommonSingleOutput()
        {
            Key = x.Key,
            DisplayName = x.Name
        }).ToListAsync();
        return articles;
    }
    
    public IQueryable<BlogDbModels.CodeEnvironment> GetListQuery(Expression<Func<BlogDbModels.CodeEnvironment, bool>>? searchWhere)
    {
        if (searchWhere != null)
        {
            return _blogDbContext.CodeEnvironments.Where(searchWhere).AsNoTracking();
        }

        return _blogDbContext.CodeEnvironments.AsNoTracking();
    }

    public async Task AddEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment)
    {
        var newTag = new BlogDbModels.CodeEnvironment()
        {
            Key = Guid.NewGuid(),
            Name = codeEnvironment.Name,
            IconUrl = string.IsNullOrWhiteSpace(codeEnvironment.IconUrl) ? null : codeEnvironment.IconUrl,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };
        await _blogDbContext.CodeEnvironments.AddAsync(newTag);
        await _blogDbContext.SaveChangesAsync();
    }

    public async Task UpdateEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment)
    {
        var currentEntity = await _blogDbContext.CodeEnvironments.FirstOrDefaultAsync(x => x.Key == codeEnvironment.Key);
        if (currentEntity != null)
        {
            currentEntity.Name = codeEnvironment.Name;
            currentEntity.UpdatedTime = DateTime.Now;
            currentEntity.IconUrl = string.IsNullOrWhiteSpace(codeEnvironment.IconUrl) ? currentEntity.IconUrl : codeEnvironment.IconUrl;
            _blogDbContext.CodeEnvironments.Update(currentEntity);
            await _blogDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteEntityAsync(Guid codeEnvironmentKey)
    {
        var currentCategory = await _blogDbContext.CodeEnvironments.FirstOrDefaultAsync(x => x.Key == codeEnvironmentKey);
        if (currentCategory != null)
        {
            _blogDbContext.CodeEnvironments.Remove(currentCategory);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}