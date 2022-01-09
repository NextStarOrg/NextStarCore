namespace NextStar.Library.Core.Abstractions;

public class AppSetting
{
    /// <summary>
    /// Serilog 日志配置
    /// </summary>
    public SerilogSettingConfig SerilogSetting { get; set; } = new SerilogSettingConfig();

    /// <summary>
    /// 数据库配置
    /// </summary>
    public DataBaseSettingConfig DataBaseSetting { get; set; } = new DataBaseSettingConfig();

    /// <summary>
    /// 认证服务器地址
    /// </summary>
    public string Authority { get; set; } = string.Empty;

    /// <summary>
    /// 环境配置key - 开发环境一般为DEBUG 生产环境一般为 PROD
    /// </summary>
    public string ConfigEnvironment { get; set; } = "PROD";
}

public class DataBaseSettingConfig
{
    /// <summary>
    /// Redis 数据库配置
    /// </summary>
    public string Redis { get; set; } = string.Empty;

    /// <summary>
    /// 账户/账号管理
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 博客数据库相关
    /// </summary>
    public string Management { get; set; } = string.Empty;
}

public class SerilogSettingConfig
{
    public string ServerName { get; set; } = "NextStar";

    /// <summary>
    /// 严重/警告错误日志配置
    /// </summary>
    public SerilogSettingConfigCommon ErrorConfig { get; set; } = new SerilogSettingConfigCommon();

    /*
     * _logger.LogTrace("Trace"); // Verbose
        _logger.LogDebug("Debug");
        _logger.LogInformation("Information");
        _logger.LogWarning("Warning");
        _logger.LogError("Error");
        _logger.LogCritical("Critical"); // == Fatal
     */
    /// <summary>
    /// 信息/审计日志配置
    /// </summary>
    public SerilogSettingConfigCommon NormalConfig { get; set; } = new SerilogSettingConfigCommon();

    /// <summary>
    /// 业务日志配置
    /// </summary>
    public SerilogSettingConfigCommon BusinessConfig { get; set; } = new SerilogSettingConfigCommon();
}

/// <summary>
/// serilog日志共通
/// </summary>
public class SerilogSettingConfigCommon
{
    /// <summary>
    /// 存放日志路径 默认：logs/error
    /// </summary>
    public string Path { get; set; } = "logs";

    /// <summary>
    /// 文件大小 20971520 = 20M
    /// </summary>
    public int FileSizeLimitBytes { get; set; } = 20971520;

    /// <summary>
    /// 存放文件数量:200
    /// </summary>
    public int RetainedFileCountLimit { get; set; } = 200;
}