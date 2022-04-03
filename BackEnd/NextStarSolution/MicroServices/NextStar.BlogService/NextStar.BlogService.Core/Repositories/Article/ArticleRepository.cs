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
            Id = x.Id,
            DisplayName = x.Title
        }).ToListAsync();
        return articles ?? new List<CommonSingleOutput>();
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
                Title = articleInput.Title,
                Description = articleInput.Description,
                PublishTime = articleInput.PublishTime,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };
            await _blogDbContext.Articles.AddAsync(article);
            await _blogDbContext.SaveChangesAsync();

            // add relation
            if (articleInput.Category != null)
            {
                var ac = new ArticleCategory()
                {
                    ArticleId = article.Id,
                    CategoryId = articleInput.Category
                };
                await _blogDbContext.ArticleCategories.AddAsync(ac);
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
        var article = await _blogDbContext.Articles.FirstOrDefaultAsync(x => x.Id == articleInput.ArticleId);
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
            article.PublishTime = articleInput.PublishTime;
            article.UpdatedTime = DateTime.Now;
            _blogDbContext.Articles.Update(article);

            var acExistList = _blogDbContext.ArticleCategories.Where(x => x.ArticleId == article.Id);
            _blogDbContext.ArticleCategories.RemoveRange(acExistList);
            // add relation
            if (articleInput.Category != null)
            {
                var ac = new ArticleCategory()
                {
                    ArticleId = article.Id,
                    CategoryId = articleInput.Category
                };
                await _blogDbContext.ArticleCategories.AddAsync(ac);
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

    public async Task DeleteEntityAsync(int articleId)
    {
        var article = await _blogDbContext.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
        if (article == null)
            return;
        var articleContent = await _blogDbContext.ArticleContents.Where(x => x.ArticleId == articleId)
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
                await _blogDbContext.ArticleCategories.FirstOrDefaultAsync(x => articleItem.Id == x.ArticleId);

            if (relationCategory != null)
            {
                var category =
                    await _blogDbContext.Categories.FirstOrDefaultAsync(x => relationCategory.CategoryId == x.Id);

                articleItem.Category = category == null ? null : _mapper.Map<ArticleCategoryItem>(category);
            }
        }
    }
}