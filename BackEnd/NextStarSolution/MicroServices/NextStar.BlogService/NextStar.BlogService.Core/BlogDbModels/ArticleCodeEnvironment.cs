using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class ArticleCodeEnvironment
    {
        public Guid ArticleKey { get; set; }
        public Guid EnvironmentKey { get; set; }
        public string Version { get; set; } = null!;

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
        public virtual CodeEnvironment EnvironmentKeyNavigation { get; set; } = null!;
    }
}
