using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class Author
    {
        public Author()
        {
            ArticleAuthors = new HashSet<ArticleAuthor>();
        }

        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual AuthorProfile AuthorProfile { get; set; } = null!;
        public virtual ICollection<ArticleAuthor> ArticleAuthors { get; set; }
    }
}
