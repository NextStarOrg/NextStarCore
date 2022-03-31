using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.Entities.ArticleContent;
using NextStar.BlogService.Core.Repositories.ArticleContent;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;

namespace NextStar.BlogService.Core.Businesses.ArticleContent;

public class ArticleContentBusiness : IArticleContentBusiness
{
    private readonly IArticleContentRepository _repository;
    public ArticleContentBusiness(IArticleContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<BlogDbModels.ArticleContent?> GetContentAsync(ArticleContentGetContentInput getContentInput)
    {
        return await _repository.GetContentAsync(getContentInput);
    }

    /// <summary>
    /// 返回的不携带content
    /// </summary>
    /// <returns></returns>
    public async Task<PageCommonDto<BlogDbModels.ArticleContent>> GetListAsync(ArticleContentSelectInput selectInput)
    {
        var acList = await _repository.GetListAsync();
        acList = acList.OrderByDescending(x => x.CreatedTime).ToList();
        var query = acList.AsQueryable().CommonPagination(selectInput);
        
        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<BlogDbModels.ArticleContent>()
        {
            Data = result,
            TotalCount = count
        };
    }

    public async Task AddAsync(ArticleContentAddInput addInput)
    {
        if (addInput.ArticleKey == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "文章",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        
        if (string.IsNullOrWhiteSpace(addInput.Content))
        {
            throw new InvalidateModelDataException()
            {
                Property = "文章内容",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }

        await _repository.AddEntityAsync(addInput);
    }
}