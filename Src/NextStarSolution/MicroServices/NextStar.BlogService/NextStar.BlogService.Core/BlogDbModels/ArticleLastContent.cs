using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class ArticleLastContent
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPublish { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string Content { get; set; } = null!;
        public DateTime ContentCreatedTime { get; set; }
    }
}
