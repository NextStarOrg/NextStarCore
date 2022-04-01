using System;
using System.Collections.Generic;

namespace NextStar.Identity.NextStarDbModels
{
    public partial class UserProfile
    {
        public int UserId { get; set; }
        public string LoginName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string Salt { get; set; } = null!;
        /// <summary>
        /// Base64(nextstar_{salt}_xA123456)
        /// </summary>
        public string PassWord { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
