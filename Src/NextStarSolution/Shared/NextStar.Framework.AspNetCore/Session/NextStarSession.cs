using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NextStar.Framework.Abstractions.Session;
using NextStar.Framework.AspNetCore.Extensions;

namespace NextStar.Framework.AspNetCore.Session
{
    public class NextStarSession : INextStarSession
    {
        private ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NextStarSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserKey => Principal.GetNextStarUserKey();
        public Guid? SessionId => Principal.GetNextStarSessionId();
        public string Name => Principal.GetNextStarName();

        public string Email => Principal.GetNextStarEmail();
        public long? Phone => Principal.GetNextStarPhone();
    }
}