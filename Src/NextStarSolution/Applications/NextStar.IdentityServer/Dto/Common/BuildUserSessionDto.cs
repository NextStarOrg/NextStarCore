using System;
using NextStar.Framework.EntityFrameworkCore.Input.Consts;

namespace NextStar.IdentityServer.Dto
{
    public class BuildUserSessionDto
    {
        public Guid UserKey { get; set; }
        public string ClientId { get; set; }
        public NextStarLoginProvider LoginProvider { get; set; }
        public long Seconds { get; set; }
    }
}