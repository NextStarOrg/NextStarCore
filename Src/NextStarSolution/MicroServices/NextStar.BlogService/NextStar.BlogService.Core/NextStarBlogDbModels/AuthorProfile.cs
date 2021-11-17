using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class AuthorProfile
    {
        public Guid AuthorKey { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }

        public virtual Author AuthorKeyNavigation { get; set; } = null!;
    }
}
