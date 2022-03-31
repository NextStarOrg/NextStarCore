using System;
using System.Collections.Generic;

namespace NextStar.SystemService.Core.AccountDbModels
{
    public partial class UserSession
    {
        public Guid SessionId { get; set; }
        public Guid UserKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpiredTime { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
