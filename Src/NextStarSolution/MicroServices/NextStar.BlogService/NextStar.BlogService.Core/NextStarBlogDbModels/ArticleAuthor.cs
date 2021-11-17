using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class ArticleAuthor
    {
        public Guid ArticleKey { get; set; }
        public Guid AuthorKey { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
        public virtual Author AuthorKeyNavigation { get; set; } = null!;
    }
}
