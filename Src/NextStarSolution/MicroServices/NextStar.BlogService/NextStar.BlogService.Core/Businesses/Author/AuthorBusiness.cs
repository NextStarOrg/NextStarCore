using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NextStar.BlogService.Core.Entities;
using NextStar.BlogService.Core.NextStarBlogDbModels;
using NextStar.BlogService.Core.Repositories;
using NextStar.Framework.EntityFrameworkCore.Exntensions;
using NextStar.Framework.EntityFrameworkCore.Output;

namespace NextStar.BlogService.Core.Businesses;

public class AuthorBusiness:IAuthorBusiness
{
    private readonly ILogger<AuthorBusiness> _logger;
    private readonly IAuthorRepository _repository;
    public AuthorBusiness(ILogger<AuthorBusiness> logger,
        IAuthorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<SelectListOutput<Author>> GetListAsync(AuthorSelectInput input)
    {
        var queryList = _repository.GetList();
        if (!string.IsNullOrWhiteSpace(input.SearchText))
        {
            queryList = queryList.Where(x => x.Name.Contains(input.SearchText));
        }
        
        if (input.Filters.Any())
        {
            queryList = queryList.Filter(input);
        }
        
        var count = queryList.Count();
        queryList = queryList.SortPagination(input);
        var data = await queryList.ToListAsync();
        return new SelectListOutput<Author>()
        {
            Count = count,
            Data = data
        };
    }
}