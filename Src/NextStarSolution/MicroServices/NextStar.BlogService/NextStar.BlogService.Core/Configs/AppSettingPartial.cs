using NextStar.Library.Core.Abstractions;

namespace NextStar.BlogService.Core.Configs;

public class AppSettingPartial:AppSetting
{
    public string? ArticleDeleteBackupPath { get; set; }
}