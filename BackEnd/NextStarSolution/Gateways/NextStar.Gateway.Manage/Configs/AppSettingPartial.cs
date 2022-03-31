using NextStar.Library.Core.Abstractions;

namespace NextStar.Gateway.Manage.Configs;

public class AppSettingPartial:AppSetting
{
    public List<string> AllowedOrigins { get; set; } = new List<string>();
}