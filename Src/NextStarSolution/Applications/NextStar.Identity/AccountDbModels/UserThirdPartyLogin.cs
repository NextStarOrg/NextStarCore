﻿using System;
using System.Collections.Generic;

namespace NextStar.Identity.AccountDbModels
{
    public partial class UserThirdPartyLogin
    {
        public int Id { get; set; }
        public Guid UserKey { get; set; }
        public int LoginType { get; set; }
        public string ThirdPartyKey { get; set; } = null!;
        public string? ThirdPartyName { get; set; }
        public string? ThirdPartyEmail { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
