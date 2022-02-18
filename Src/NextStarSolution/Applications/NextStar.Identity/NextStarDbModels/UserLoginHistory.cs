using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class UserLoginHistory
    {
        public int Id { get; set; }
        public Guid UserKey { get; set; }
        public string LoginType { get; set; } = null!;
        public string UserAgent { get; set; } = null!;
        public string? IpV4 { get; set; }
        public string? IpV6 { get; set; }
        public Guid SessionId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
