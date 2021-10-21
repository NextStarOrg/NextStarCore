using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.IdentityServer.NextStarAccountDbModels
{
    public partial class ThirdParty
    {
        public int Id { get; set; }
        public Guid UserKey { get; set; }
        public string Key { get; set; }
        public int Provider { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual User UserKeyNavigation { get; set; }
    }
}
