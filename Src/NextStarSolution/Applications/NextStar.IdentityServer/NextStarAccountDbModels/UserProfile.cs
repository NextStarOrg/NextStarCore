using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.IdentityServer.NextStarAccountDbModels
{
    public partial class UserProfile
    {
        public Guid UserKey { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool Status { get; set; }
        public string LoginName { get; set; }
        public Guid Salt { get; set; }
        public string PassWord { get; set; }

        public virtual User UserKeyNavigation { get; set; }
    }
}
