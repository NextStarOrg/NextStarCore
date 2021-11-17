using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class ArticleCategory
    {
        public Guid ArticleKey { get; set; }
        public Guid CategoryKey { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
        public virtual Category CategoryKeyNavigation { get; set; } = null!;
    }
}
