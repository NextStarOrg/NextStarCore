using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.ManageService.Core.NextStarAccountDbModels
{
    public partial class UserSession
    {
        public Guid Id { get; set; }
        public Guid UserKey { get; set; }
        public int Provider { get; set; }
        public long Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
