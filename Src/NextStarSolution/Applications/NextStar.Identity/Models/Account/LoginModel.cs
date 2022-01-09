namespace NextStar.Identity.Models;

public class LoginModel
{
    /// <summary>
    /// 登录名
    /// </summary>
    public string LoginName { get; set; } = null!;
    /// <summary>
    /// 登录密码
    /// </summary>
    public string LoginPassword { get; set; } = null!;

    public string ReturnUrl { get; set; } = string.Empty;
}