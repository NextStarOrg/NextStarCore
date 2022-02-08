using Microsoft.Extensions.Logging;

namespace NextStar.SystemService.Core.Businesses.ThirdPartyLogin;

public class ThirdPartyLoginBusiness:IThirdPartyLoginBusiness
{
    private readonly ILogger<ThirdPartyLoginBusiness> _logger;
    public ThirdPartyLoginBusiness(ILogger<ThirdPartyLoginBusiness> logger)
    {
        _logger = logger;
    }
}