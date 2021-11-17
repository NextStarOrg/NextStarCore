using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class ArticlePartial
    {
        public Guid ArticleKey { get; set; }
        public string AliasUrl { get; set; } = null!;
        public bool IsPublic { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? AssignCreatedTime { get; set; }
        public DateTime? AssignUpdatedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
    }
}
