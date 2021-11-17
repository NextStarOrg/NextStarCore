using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class ArticleTag
    {
        public Guid ArticleKey { get; set; }
        public Guid TagKey { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
        public virtual Tag TagKeyNavigation { get; set; } = null!;
    }
}
