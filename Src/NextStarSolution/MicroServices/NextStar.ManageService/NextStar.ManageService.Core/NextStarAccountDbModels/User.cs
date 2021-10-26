using System;
using System.Collections.Generic;

#nullable disable

namespace NextStar.ManageService.Core.NextStarAccountDbModels
{
    public partial class User
    {
        public User()
        {
            ThirdParties = new HashSet<ThirdParty>();
            UserHistories = new HashSet<UserHistory>();
        }

        public int Id { get; set; }
        public Guid Key { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<ThirdParty> ThirdParties { get; set; }
        public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}
