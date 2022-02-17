using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.DbContexts;
using NextStar.BlogService.Core.Entities.ArticleContent;
using NextStar.Library.MicroService.Exceptions;

namespace NextStar.BlogService.Core.Repositories.ArticleContent;

public class ArticleContentRepository : IArticleContentRepository
{
    private readonly BlogDbContext _blogDbContext;
    public ArticleContentRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }

    public async Task<BlogDbModels.ArticleContent?> GetContentAsync(ArticleContentGetContentInput getContentInput)
    {
        return await _blogDbContext.ArticleContents.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == getContentInput.ContentId && x.ArticleKey == getContentInput.ArticleKey);
    }
    
    public async Task<List<BlogDbModels.ArticleContent>> GetListAsync()
    {
        return await _blogDbContext.ArticleContents.AsNoTracking().Select(x => new BlogDbModels.ArticleContent()
        {
            ArticleKey = x.ArticleKey,
            CommitMessage = x.CommitMessage,
            Id = x.Id,
            Content = string.Empty
        }).ToListAsync();
    }

    public async Task AddEntityAsync(ArticleContentAddInput addInput)
    {
        var article = await _blogDbContext.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Key == addInput.ArticleKey);
        if (article == null)
        {
            throw new InvalidateModelDataException()
            {
                Property = "文章",
                Type = InvalidateModelDataException.InvalidateType.NotFound
            };
        }

        var articleContent = new BlogDbModels.ArticleContent()
        {
            ArticleKey = addInput.ArticleKey,
            Content = addInput.Content,
            CommitMessage = addInput.CommitMessage
        };
        await _blogDbContext.ArticleContents.AddAsync(articleContent);
        await _blogDbContext.SaveChangesAsync();
    }
}