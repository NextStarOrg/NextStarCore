using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NextStar.BlogService.Core.BlogDbModels;
using NextStar.BlogService.Core.Configs;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.Article;

namespace NextStar.BlogService.Core.Repositories.Article;

public class ArticleRepository
{
    private readonly BlogDbContext _blogDbContext;
    private readonly ILogger<ArticleRepository> _logger;
    private readonly IConfiguration _configuration;

    public ArticleRepository(BlogDbContext blogDbContext,
        ILogger<ArticleRepository> logger)
    {
        _blogDbContext = blogDbContext;
        _logger = logger;
    }

    public async Task<IQueryable<BlogDbModels.Article>> SelectEntity(ArticleSelectInput selectInput)
    {
        var query = _blogDbContext.Articles.AsQueryable();
        if (!string.IsNullOrWhiteSpace(selectInput.SearchText))
        {
            query = query.Where(x =>
                x.Title.Contains(selectInput.SearchText) || x.Description.Contains(selectInput.SearchText));
        }

        if (selectInput.StartTime.HasValue && selectInput.EndTime.HasValue &&
            DateTime.Compare(selectInput.StartTime.Value, selectInput.EndTime.Value) <=0)
        {
            query = query.Where(x =>
                x.Title.Contains(selectInput.SearchText) || x.Description.Contains(selectInput.SearchText));
        }

        if (selectInput.IsPublish.HasValue)
        {
            query = query.Where(x => x.IsPublish == selectInput.IsPublish);
        }

        return query.AsNoTracking();
    }
    public async Task<bool> AddEntity(ArticleInput articleInput)
    {
        await using var t = await _blogDbContext.Database.BeginTransactionAsync();
        try
        {
            // create article
            var article = new BlogDbModels.Article()
            {
                Key = Guid.NewGuid(),
                Title = articleInput.Title,
                Description = articleInput.Description,
                IsPublish = articleInput.IsPublish,
                PublishTime = articleInput.PublishTime,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };
            await _blogDbContext.Articles.AddAsync(article);
            await _blogDbContext.SaveChangesAsync();

            // add relation
            if (articleInput.Categories.Count > 0)
            {
                var acList = articleInput.Categories.Select(x => new ArticleCategory()
                {
                    ArticleKey = article.Key,
                    CategoryKey = x
                }).ToList();
                await _blogDbContext.ArticleCategories.AddRangeAsync(acList);
            }

            if (articleInput.Tags.Count > 0)
            {
                var atList = articleInput.Tags.Select(x => new ArticleTag()
                {
                    ArticleKey = article.Key,
                    TagKey = x
                }).ToList();
                await _blogDbContext.ArticleTags.AddRangeAsync(atList);
            }

            if (articleInput.CodeEnvironments.Count > 0)
            {
                var aceList = articleInput.CodeEnvironments.Select(x => new ArticleCodeEnvironment()
                {
                    ArticleKey = article.Key,
                    EnvironmentKey = x
                }).ToList();
                await _blogDbContext.ArticleCodeEnvironments.AddRangeAsync(aceList);
            }

            await _blogDbContext.SaveChangesAsync();
            await t.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await t.RollbackAsync();
            _logger.LogError(e, "ERROR 30-040-020  add article entity error");
            return false;
        }
    }

    public async Task<bool> UpdateEntity(ArticleInput articleInput)
    {
        var article = await _blogDbContext.Articles.FirstOrDefaultAsync(x => x.Key == articleInput.ArticleKey);
        if (article == null)
        {
            return false;
        }

        await using var t = await _blogDbContext.Database.BeginTransactionAsync();

        try
        {
            // update article
            article.Title = articleInput.Title;
            article.Description = articleInput.Description;
            article.IsPublish = articleInput.IsPublish;
            article.PublishTime = articleInput.PublishTime;
            article.UpdatedTime = DateTime.Now;
            _blogDbContext.Articles.Update(article);

            var acExistList = _blogDbContext.ArticleCategories.Where(x => x.ArticleKey == article.Key);
            _blogDbContext.ArticleCategories.RemoveRange(acExistList);
            // add relation
            if (articleInput.Categories.Count > 0)
            {
                var acList = articleInput.Categories.Select(x => new ArticleCategory()
                {
                    ArticleKey = article.Key,
                    CategoryKey = x
                });
                await _blogDbContext.ArticleCategories.AddRangeAsync(acList);
            }

            var atExistList = _blogDbContext.ArticleCategories.Where(x => x.ArticleKey == article.Key);
            _blogDbContext.ArticleCategories.RemoveRange(acExistList);
            if (articleInput.Tags.Count > 0)
            {
                var atList = articleInput.Tags.Select(x => new ArticleTag()
                {
                    ArticleKey = article.Key,
                    TagKey = x
                });
                await _blogDbContext.ArticleTags.AddRangeAsync(atList);
            }

            var aceExistList = _blogDbContext.ArticleCodeEnvironments.Where(x => x.ArticleKey == article.Key);
            _blogDbContext.ArticleCodeEnvironments.RemoveRange(aceExistList);
            if (articleInput.CodeEnvironments.Count > 0)
            {
                var aceList = articleInput.CodeEnvironments.Select(x => new ArticleCodeEnvironment()
                {
                    ArticleKey = article.Key,
                    EnvironmentKey = x
                });
                await _blogDbContext.ArticleCodeEnvironments.AddRangeAsync(aceList);
            }

            await _blogDbContext.SaveChangesAsync();
            await t.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await t.RollbackAsync();
            _logger.LogError(e, "ERROR 30-040-030  add article entity error");
            return false;
        }
    }

    public async Task DeleteEntity(Guid articleKey)
    {
        var article = await _blogDbContext.Articles.FirstOrDefaultAsync(x => x.Key == articleKey);
        if (article == null)
            return;
        var articleContent = await _blogDbContext.ArticleContents.Where(x => x.ArticleKey == articleKey)
            .OrderByDescending(x => x.CreatedTime).FirstOrDefaultAsync();
        if (articleContent == null)
            return;
        // 删除之前对内容进行备份保存
        var appSetting = _configuration.Get<AppSettingPartial>();
        var path = Path.Combine(appSetting.ArticleDeleteBackupPath ?? string.Empty,
            $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}{article.Title}.md");
        await File.WriteAllTextAsync(path, articleContent.Content);

        _logger.LogInformation("ERROR 30-040-040 delete article {@article} start", article);
        _blogDbContext.Articles.RemoveRange(article);
        await _blogDbContext.SaveChangesAsync();
        _logger.LogInformation("ERROR 30-040-040 delete article {@article} finish", article);
    }
}