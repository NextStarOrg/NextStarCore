using System;
using System.Collections.Generic;

namespace NextStar.SystemService.Core.AccountDbModels
{
    public partial class UserThirdPartyLogin
    {
        public int Id { get; set; }
        public Guid UserKey { get; set; }
        public string LoginType { get; set; } = null!;
        public string ThirdPartyKey { get; set; } = null!;
        public string? ThirdPartyName { get; set; }
        public string? ThirdPartyEmail { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
