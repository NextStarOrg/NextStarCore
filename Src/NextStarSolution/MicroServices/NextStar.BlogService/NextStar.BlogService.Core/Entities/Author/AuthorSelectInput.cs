﻿using NextStar.Framework.EntityFrameworkCore.Filter;
using NextStar.Framework.EntityFrameworkCore.Input;
using NextStar.Framework.EntityFrameworkCore.Sort;

namespace NextStar.BlogService.Core.Entities;

public class AuthorSelectInput:ICommonInput,IFilterInput
{
    public string SearchText { get; set; } = string.Empty;
    public List<SortDescriptor> Sorts { get; set; } = new List<SortDescriptor>();
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 1;
    public List<FilterDescriptor> Filters { get; set; } = new List<FilterDescriptor>();
}