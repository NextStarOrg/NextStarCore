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

    public async Task<bool> CreateAsync(AuthorCreatInput input)
    {
        var key = Guid.NewGuid();
        var author = new Author()
        {
            Key = key,
            Name = input.Name,
            CreatedTime = DateTime.UtcNow,
            AuthorProfile = new AuthorProfile()
            {
                Email = input.Profile.Email,
                Url = input.Profile.Url
            }
        };
        await _repository.CreateAsync(author);
        return true;
    }

    public async Task<bool> UpdateAsync(AuthorUpdateInput input)
    {
        var author = await _repository.GetWithProfileByKeyAsync(input.Key);
        if (author != null)
        {
            author.Name = input.Name;
            author.AuthorProfile.Email = input.Profile.Email;
            author.AuthorProfile.Url = input.Profile.Url;
            await _repository.UpdateAuthorAsync(author);
            return true;
        }

        return false;
    }

    public async Task<SelectListOutput<Author>> GetListAsync(AuthorSelectInput input)
    {
        var queryList = _repository.GetAuthors();
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