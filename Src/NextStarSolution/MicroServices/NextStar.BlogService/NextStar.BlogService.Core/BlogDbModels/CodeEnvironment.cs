using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class CodeEnvironment
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; } = null!;
        public string? IconUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
