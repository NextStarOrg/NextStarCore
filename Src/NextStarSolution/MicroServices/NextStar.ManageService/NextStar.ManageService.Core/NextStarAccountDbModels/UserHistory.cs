using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.ManageService.Core.NextStarAccountDbModels
{
    public partial class UserHistory
    {
        public int Id { get; set; }
        public Guid UserKey { get; set; }
        public Guid SessionId { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string Fingerprint { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string Platform { get; set; }
        public string SystemName { get; set; }
        public string SystemVersion { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual User UserKeyNavigation { get; set; }
    }
}
