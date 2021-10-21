using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace NextStar.IdentityServer.Filters
{
    public class NextStarCustomAuthorizeRequestValidator:ICustomAuthorizeRequestValidator
    {
        public async Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}