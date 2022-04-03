using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.Entities.Article;
using NextStar.BlogService.Core.Repositories.Article;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;

namespace NextStar.BlogService.Core.Businesses.Article;

public class ArticleBusiness : IArticleBusiness
{
    private readonly IArticleRepository _repository;
    private readonly IMapper _mapper;
    public ArticleBusiness(IArticleRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CommonSingleOutput>> SearchSingleAsync(string? searchText)
    {
        return await _repository.SearchSingleAsync(searchText);
    }

    public async Task<PageCommonDto<ArticleItem>> SelectArticleAsync(ArticleSelectInput selectInput)
    {
        var articleQuery = await _repository.SelectEntityAsync();
        if (!string.IsNullOrWhiteSpace(selectInput.SearchText))
        {
            articleQuery = articleQuery.Where(x =>
                x.Title.Contains(selectInput.SearchText) || x.Description.Contains(selectInput.SearchText));
        }

        // if (selectInput.StartTime.HasValue && selectInput.EndTime.HasValue &&
        //     DateTime.Compare(selectInput.StartTime.Value, selectInput.EndTime.Value) <=0)
        // {
        //     articleQuery = articleQuery.Where(x =>
        //         x.Title.Contains(selectInput.SearchText) || x.Description.Contains(selectInput.SearchText));
        // }

        if (selectInput.PublishTime.HasValue)
        {
            articleQuery = articleQuery.Where(x => x.PublishTime < selectInput.PublishTime.Value);
        }

        var articles = _mapper.Map<List<ArticleItem>>(await articleQuery.ToListAsync());

        await _repository.GetArticleCategoryByArticles(articles);

        if (selectInput.CategoryKeys.Count > 0)
        {
            articles = articles.Where(x => x.Category != null && selectInput.CategoryKeys.Contains(x.Category.Key)).ToList();
        }
        
        var query = articles.AsQueryable();
        query = query.CommonPageSort(selectInput, "Id desc");

        var result = query.ToList();
        var count = query.Count();
        return new PageCommonDto<ArticleItem>()
        {
            Data = result,
            TotalCount = count
        };
    }

    public async Task AddAsync(ArticleInput articleInput)
    {
        if (string.IsNullOrWhiteSpace(articleInput.Title))
        {
            throw new InvalidateModelDataException()
            {
                Property = "标题",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        
        if (string.IsNullOrWhiteSpace(articleInput.Description))
        {
            throw new InvalidateModelDataException()
            {
                Property = "标题",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }

        await _repository.AddEntityAsync(articleInput);
    }
    
    public async Task UpdateAsync(ArticleInput articleInput)
    {
        if (string.IsNullOrWhiteSpace(articleInput.Title))
        {
            throw new InvalidateModelDataException()
            {
                Property = "标题",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        
        if (string.IsNullOrWhiteSpace(articleInput.Description))
        {
            throw new InvalidateModelDataException()
            {
                Property = "标题",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        
        await _repository.AddEntityAsync(articleInput);
    }

    public async Task DeleteAsync(int articleId)
    {
        await _repository.DeleteEntityAsync(articleId);
    }
}