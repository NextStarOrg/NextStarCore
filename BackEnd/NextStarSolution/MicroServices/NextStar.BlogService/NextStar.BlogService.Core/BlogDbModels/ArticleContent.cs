using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class ArticleContent
    {
        public long Id { get; set; }
        public int ArticleId { get; set; }
        public string Content { get; set; } = null!;
        public string? CommitMessage { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Article Article { get; set; } = null!;
    }
}
