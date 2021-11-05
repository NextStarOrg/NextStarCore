using System.Collections.Generic;
using NextStar.Framework.Abstractions.AppSetting;
using NextStar.Framework.Core.Consts;

namespace NextStar.Gateway.Configs
{
    public class AppSettingPartial:AppSetting
    {
        public List<string> AllowedOrigins { get; set; }
    }
}