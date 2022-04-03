using NextStar.Library.MicroService.Inputs;

namespace NextStar.BlogService.Core.Entities.Article;

public class ArticleSelectInput : PageSearchTextInput
{
    /// <summary>
    /// 开始时间：更新或者创建时间在其中都出来
    /// </summary>
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime? PublishTime { get; set; }
    /// <summary>
    /// 分类
    /// </summary>
    public List<Guid> CategoryKeys { get; set; } = new List<Guid>();
}