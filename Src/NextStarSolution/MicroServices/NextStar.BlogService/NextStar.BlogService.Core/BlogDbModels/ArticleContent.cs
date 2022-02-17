using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class ArticleContent
    {
        public Guid ArticleKey { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string? CommitMessage { get; set; }
        public int Id { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
    }
}
