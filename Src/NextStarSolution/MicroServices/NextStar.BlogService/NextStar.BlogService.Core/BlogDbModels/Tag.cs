using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class Tag
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; } = null!;
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
