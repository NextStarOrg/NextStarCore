using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class UserLoginHistory
    {
        public int UserId { get; set; }
        public string LoginType { get; set; } = null!;
        public string? UserAgent { get; set; }
        public string FingerprintId { get; set; } = null!;
        public string? OtherInfo { get; set; }
        public string? IpV4 { get; set; }
        public string? IpV6 { get; set; }
        public string SessionId { get; set; } = null!;
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
