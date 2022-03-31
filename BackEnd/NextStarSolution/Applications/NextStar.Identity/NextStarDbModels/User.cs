using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class User
    {
        public User()
        {
            UserLoginHistories = new HashSet<UserLoginHistory>();
            UserThirdPartyLogins = new HashSet<UserThirdPartyLogin>();
        }

        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual UserProfile UserProfile { get; set; } = null!;
        public virtual ICollection<UserLoginHistory> UserLoginHistories { get; set; }
        public virtual ICollection<UserThirdPartyLogin> UserThirdPartyLogins { get; set; }
    }
}
