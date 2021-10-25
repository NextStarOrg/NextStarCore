using System.ComponentModel.DataAnnotations;

namespace NextStar.IdentityServer.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "登录名必须输入")]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码必须输入")]
        public string LoginPassword { get; set; }
        
        public string ReturnUrl { get; set; }
        
        public bool IsError { get; set; }
    }
}