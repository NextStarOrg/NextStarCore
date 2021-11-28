using IdentityServer4.Models;
using NextStar.Library.Core.Abstractions;

namespace NextStar.Identity.Configs;

public class AppSettingPartial:AppSetting
{
    public IdentityServer IdentityServer { get; set; } = null!;
    public Certificates Certificates { get; set; } = null!;
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
    public List<Client> Clients { get; set; } = new List<Client>();
}
public class Certificates
{
    public string Path { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class CdnSettingConfig
{
    /// <summary>
    /// CDN 名称
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// 地址URL
    /// </summary>
    public string Url { get; set; } = null!;
    /// <summary>
    /// 完整性值验证
    /// </summary>
    public string Integrity { get; set; } = null!;
}