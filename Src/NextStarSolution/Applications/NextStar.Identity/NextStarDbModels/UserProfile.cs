using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class UserProfile
    {
        public Guid UserKey { get; set; }
        public string LoginName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public Guid? Salt { get; set; }
        public string? PassWord { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
