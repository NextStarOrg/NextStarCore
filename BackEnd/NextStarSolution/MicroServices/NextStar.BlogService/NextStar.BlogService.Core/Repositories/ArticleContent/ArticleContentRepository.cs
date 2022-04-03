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
            .FirstOrDefaultAsync(x => x.Id == getContentInput.ContentId && x.ArticleId == getContentInput.ArticleId);
    }
    
    public async Task<List<BlogDbModels.ArticleContent>> GetListAsync(int articleId)
    {
        return await _blogDbContext.ArticleContents.AsNoTracking().Where(x=>x.ArticleId == articleId).Select(x => new BlogDbModels.ArticleContent()
        {
            Id = x.Id,
            CommitMessage = x.CommitMessage,
            Content = string.Empty
        }).ToListAsync();
    }

    public async Task AddEntityAsync(ArticleContentAddInput addInput)
    {
        var article = await _blogDbContext.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == addInput.ArticleId);
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
            ArticleId = addInput.ArticleId,
            Content = addInput.Content,
            CommitMessage = addInput.CommitMessage
        };
        await _blogDbContext.ArticleContents.AddAsync(articleContent);
        await _blogDbContext.SaveChangesAsync();
    }
}