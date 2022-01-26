using System.ComponentModel.DataAnnotations;

namespace NextStar.Identity.Models;

public class LoginModel
{
    /// <summary>
    /// 登录名
    /// </summary>
    [Required(ErrorMessage = "登录名必须输入")]
    public string LoginName { get; set; } = null!;
    /// <summary>
    /// 登录密码
    /// </summary>
    [Required(ErrorMessage = "密码必须输入")]
    public string LoginPassword { get; set; } = null!;

    public string ReturnUrl { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}