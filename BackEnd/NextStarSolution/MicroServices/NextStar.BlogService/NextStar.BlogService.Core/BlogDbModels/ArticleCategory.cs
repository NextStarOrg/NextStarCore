using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class ArticleCategory
    {
        public int ArticleId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Article Article { get; set; } = null!;
        public virtual Category? Category { get; set; }
    }
}
