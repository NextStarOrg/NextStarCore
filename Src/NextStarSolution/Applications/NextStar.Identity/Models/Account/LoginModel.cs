namespace NextStar.Identity.Models;

public class LoginModel
{
    /// <summary>
    /// 登录名
    /// </summary>
    public string LoginName { get; set; }
    /// <summary>
    /// 登录密码
    /// </summary>
    public string LoginPassword { get; set; }
    public string ReturnUrl { get; set; }
}