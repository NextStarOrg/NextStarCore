using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class Article
    {
        public Article()
        {
            ArticleContents = new HashSet<ArticleContent>();
        }

        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedTim { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual ArticleAuthor ArticleAuthor { get; set; } = null!;
        public virtual ArticlePartial ArticlePartial { get; set; } = null!;
        public virtual ICollection<ArticleContent> ArticleContents { get; set; }
    }
}
