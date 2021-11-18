using System.Collections.Generic;
using IdentityServer4.Models;
using NextStar.Framework.Abstractions.AppSetting;
using NextStar.Framework.EntityFrameworkCore.Input.Consts;

namespace NextStar.IdentityServer.Configs
{
    public class AppSettingPartial:AppSetting
    {
        public IdentityServer IdentityServer { get; set; }
        public Certificates Certificates { get; set; }
        /// <summary>
        /// js cdn库
        /// </summary>
        public ICollection<CdnSettingConfig> JavaScript { get; set; } = new List<CdnSettingConfig>();
        /// <summary>
        /// css cdn库
        /// </summary>
        public ICollection<CdnSettingConfig> StyleSheet { get; set; } = new List<CdnSettingConfig>();

    }

    public class IdentityServer
    {
        public List<Client> Clients { get; set; }
    }
    public class Certificates
    {
        public string Path { get; set; }
        public string Password { get; set; }
    }

    public class CdnSettingConfig
    {
        /// <summary>
        /// CDN 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 完整性值验证
        /// </summary>
        public string Integrity { get; set; }
    }
}