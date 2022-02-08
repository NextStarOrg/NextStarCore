using NextStar.Library.Core.Consts;
using NextStar.Library.MicroService.Inputs;

namespace NextStar.SystemService.Core.Entities.ThirdPartyLogin;

public class ThirdPartyLoginSelectInput:PageSearchTextInput
{
    /// <summary>
    /// None 则为全部筛选（第三方登录表中不存在None类型，所以使用None在此处代表全部）
    /// </summary>
    public NextStarLoginType LoginType { get; set; }
}