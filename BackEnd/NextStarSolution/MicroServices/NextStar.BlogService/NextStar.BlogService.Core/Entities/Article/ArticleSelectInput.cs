using NextStar.Library.MicroService.Inputs;

namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleSelectInput : PageSearchTextInput
{
    /// <summary>
    /// 开始时间：更新或者创建时间在其中都出来
    /// </summary>
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool? IsPublish { get; set; }
    /// <summary>
    /// 分类
    /// </summary>
    public List<Guid> CategoryKeys { get; set; } = new List<Guid>();
    /// <summary>
    /// 标签
    /// </summary>
    public List<Guid> TagKeys  { get; set; } = new List<Guid>();
    /// <summary>
    /// 环境
    /// </summary>
    public List<Guid> CodeEnvironmentKeys { get; set; } = new List<Guid>();
}