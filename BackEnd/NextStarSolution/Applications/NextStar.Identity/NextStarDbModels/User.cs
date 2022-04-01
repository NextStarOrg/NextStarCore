using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual UserProfile UserProfile { get; set; } = null!;
    }
}
