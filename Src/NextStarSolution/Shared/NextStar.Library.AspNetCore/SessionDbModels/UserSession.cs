using System;
using System.Collections.Generic;

namespace NextStar.Library.AspNetCore.SessionDbModels
{
    public partial class UserSession
    {
        public Guid SessionId { get; set; }
        public Guid UserKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
