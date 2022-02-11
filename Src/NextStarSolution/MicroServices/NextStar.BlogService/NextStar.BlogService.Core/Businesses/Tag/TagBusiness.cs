﻿using Microsoft.EntityFrameworkCore;
using NextStar.BlogService.Core.Entities.Tag;
using NextStar.BlogService.Core.Repositories.Tag;
using NextStar.Library.MicroService.Exceptions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;

namespace NextStar.BlogService.Core.Businesses.Tag;

public class TagBusiness : ITagBusiness
{
    private readonly ITagRepository _repository;
    public TagBusiness(ITagRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageCommonDto<BlogDbModels.Tag>> GetListAsync(TagSelectInput selectInput)
    {
        var query = string.IsNullOrWhiteSpace(selectInput.SearchText)
            ? _repository.GetListQuery(null)
            : _repository.GetListQuery(x => x.Name.Contains(selectInput.SearchText));

        query = query.CommonPageSort(selectInput, "Id asc");

        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<BlogDbModels.Tag>()
        {
            Data = result,
            TotalCount = count
        };
    }
    
    public async Task AddAsync(BlogDbModels.Tag tag)
    {
        if (string.IsNullOrWhiteSpace(tag.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.AddEntityAsync(tag);
    }
    
    public async Task UpdateAsync(BlogDbModels.Tag tag)
    {
        if (tag.Key == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }
        if (string.IsNullOrWhiteSpace(tag.Name))
        {
            throw new InvalidateModelDataException()
            {
                Property = "名称",
                Type = InvalidateModelDataException.InvalidateType.Required
            };
        }
        await _repository.UpdateEntityAsync(tag);
    }

    public async Task DeleteAsync(Guid tagKey)
    {
        if (tagKey == Guid.Empty)
        {
            throw new InvalidateModelDataException()
            {
                Property = "分类主键",
                Type = InvalidateModelDataException.InvalidateType.IncorrectValue
            };
        }

        await _repository.DeleteEntityAsync(tagKey);
    }
}