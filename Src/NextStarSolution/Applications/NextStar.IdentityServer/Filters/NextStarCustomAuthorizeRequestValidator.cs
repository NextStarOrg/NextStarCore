using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using NextStar.Framework.AspNetCore.Extensions;
using NextStar.Framework.AspNetCore.Stores;

namespace NextStar.IdentityServer.Filters
{
    public class NextStarCustomAuthorizeRequestValidator:ICustomAuthorizeRequestValidator
    {
        private readonly INextStarSessionStore _nextStarSessionStore;
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        protected readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <value>
        /// The HTTP context.
        /// </value>
        protected HttpContext HttpContext => HttpContextAccessor.HttpContext;

        public NextStarCustomAuthorizeRequestValidator(
            INextStarSessionStore nextStarSessionStore,
            IHttpContextAccessor httpContextAccessor)
        {
            _nextStarSessionStore = nextStarSessionStore;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            var user = context.Result.ValidatedRequest.Subject;
            if (user is { Identity: { IsAuthenticated: true } })
            {
                var sessionId = user.GetNextStarSessionId();
                // SessionId存在 并且不在数据库中存在 则强制退出
                if (sessionId != null && !await _nextStarSessionStore.IsExistsAsync(sessionId.Value))
                {
                    context.Result.ValidatedRequest.Subject = Principal.Anonymous;
                    await HttpContext.SignOutAsync();
                }
            }
        }
    }
}