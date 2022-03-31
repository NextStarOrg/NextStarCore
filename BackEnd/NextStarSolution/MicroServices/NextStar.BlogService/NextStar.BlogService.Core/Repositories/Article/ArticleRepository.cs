using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NextStar.BlogService.Core.BlogDbModels;
using NextStar.BlogService.Core.Configs;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.Article;
using NextStar.Library.MicroService.Outputs;

namespace NextStar.BlogService.Core.Repositories.Article;

public class ArticleRepository : IArticleRepository
{
    private readonly BlogDbContext _blogDbContext;
    private readonly ILogger<ArticleRepository> _logger;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ArticleRepository(BlogDbContext blogDbContext,
        ILogger<ArticleRepository> logger,
        IConfiguration configuration,
        IMapper mapper)
    {
        _blogDbContext = blogDbContext;
        _logger = logger;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText)
    {
        var articles = await _blogDbContext.Articles.Where(x => x.Title.Contains(searchText ?? string.Empty)).AsNoTracking().OrderByDescending(x=>x.UpdatedTime).Select(x=> new CommonSingleOutput()
        {
            Key = x.Key,
            DisplayName = x.Title
        }).ToListAsync();
        return articles;
    }
    public async Task<IQueryable<BlogDbModels.Article>> SelectEntityAsync()
    {
        var query = _blogDbContext.Articles.AsQueryable().AsNoTracking();
        return await Task.FromResult(query);
    }

    public async Task<bool> AddEntityAsync(ArticleInput articleInput)
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
            if (articleInput.Category != null && articleInput.Category != Guid.Empty)
            {
                var ac = new ArticleCategory()
                {
                    ArticleKey = article.Key,
                    CategoryKey = articleInput.Category.Value
                };
                await _blogDbContext.ArticleCategories.AddAsync(ac);
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

    public async Task<bool> UpdateEntityAsync(ArticleInput articleInput)
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
            if (articleInput.Category != null && articleInput.Category != Guid.Empty)
            {
                var ac = new ArticleCategory()
                {
                    ArticleKey = article.Key,
                    CategoryKey = articleInput.Category.Value
                };
                await _blogDbContext.ArticleCategories.AddAsync(ac);
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

    public async Task DeleteEntityAsync(Guid articleKey)
    {
        var article = await _blogDbContext.Articles.FirstOrDefaultAsync(x => x.Key == articleKey);
        if (article == null)
            return;
        var articleContent = await _blogDbContext.ArticleContents.Where(x => x.ArticleKey == articleKey)
            .OrderByDescending(x => x.CreatedTime).FirstOrDefaultAsync();
        if (articleContent != null)
        {
            // 删除之前对内容进行备份保存
            var appSetting = _configuration.Get<AppSettingPartial>();
            var path = Path.Combine(appSetting.ArticleDeleteBackupPath ?? string.Empty);
                //$"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}{article.Title}.md");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}_{article.Title}.md");
            var fs = File.OpenWrite(filePath);
            await fs.WriteAsync(Encoding.UTF8.GetBytes(articleContent.Content));
            fs.Close();
        }

        _logger.LogInformation("ERROR 30-040-040 delete article {@article} start", article);
        _blogDbContext.Articles.RemoveRange(article);
        await _blogDbContext.SaveChangesAsync();
        _logger.LogInformation("ERROR 30-040-040 delete article {@article} finish", article);
    }

    public async Task GetArticleCategoryByArticles(List<ArticleItem> articleItems)
    {
        foreach (var articleItem in articleItems)
        {
            var relationCategory =
                await _blogDbContext.ArticleCategories.FirstOrDefaultAsync(x => articleItem.Key == x.ArticleKey);

            if (relationCategory != null)
            {
                var category =
                    await _blogDbContext.Categories.FirstOrDefaultAsync(x => relationCategory.CategoryKey == x.Key);

                articleItem.Category = category == null ? null : _mapper.Map<ArticleCategoryItem>(category);
            }
        }
    }

    public async Task GetArticleTagByArticles(List<ArticleItem> articleItems)
    {
        foreach (var articleItem in articleItems)
        {
            var relationTagKeys = await _blogDbContext.ArticleTags.Where(x => articleItem.Key == x.ArticleKey)
                .Select(x => x.TagKey).ToListAsync();

            if (relationTagKeys.Count > 0)
            {
                var tags = await _blogDbContext.Tags.Where(x => relationTagKeys.Contains(x.Key)).ToListAsync();
                articleItem.Tags =
                    tags.Count == 0 ? new List<ArticleTagItem>() : _mapper.Map<List<ArticleTagItem>>(tags);
            }
        }
    }

    public async Task GetArticleCodeEnvironmentByArticles(List<ArticleItem> articleItems)
    {
        foreach (var articleItem in articleItems)
        {
            var relationCodeEnvironments = await _blogDbContext.ArticleCodeEnvironments
                .Where(x => articleItem.Key == x.ArticleKey).ToDictionaryAsync(x => x.EnvironmentKey, x => x.Version);

            if (relationCodeEnvironments.Count > 0)
            {
                var keys = relationCodeEnvironments.Keys.ToList();
                var codeEnvironmentItems = await _blogDbContext.CodeEnvironments.Where(x => keys.Contains(x.Key)).Select(x=> new ArticleCodeEnvironmentItem()
                {
                    Key = x.Key,
                    Name = x.Name,
                    IconUrl = x.IconUrl,
                    Version = relationCodeEnvironments[x.Key]
                }).ToListAsync();
                articleItem.CodeEnvironment = codeEnvironmentItems;
            }
        }
    }
}