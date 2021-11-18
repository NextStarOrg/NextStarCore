using NextStar.Framework.EntityFrameworkCore.Input.Consts;

namespace NextStar.Framework.Abstractions.OAuth2
{
    public class RequestTokenParameter
    {
        public string AuthorizationCode { get; set; }
        public string State { get; set; }
        public NextStarLoginProvider Provider { get; set; }
    }
}