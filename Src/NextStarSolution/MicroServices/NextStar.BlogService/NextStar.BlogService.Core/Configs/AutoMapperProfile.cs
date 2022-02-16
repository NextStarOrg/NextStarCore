using AutoMapper;
using NextStar.BlogService.Core.BlogDbModels;
using NextStar.BlogService.Core.Entities.Article;

namespace NextStar.BlogService.Core.Configs;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        #region Article

        CreateMap<Article, ArticleItem>();
        CreateMap<Category, ArticleCategoryItem>();
        CreateMap<Tag, ArticleTagItem>();

        #endregion
    }
}