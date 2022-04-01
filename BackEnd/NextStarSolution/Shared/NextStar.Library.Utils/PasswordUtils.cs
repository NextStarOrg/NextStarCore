using System.Security.Cryptography;
using System.Text;

namespace NextStar.Library.Utils;

public static class PasswordUtils
{
    /// <summary>
    /// 对密码加盐加密
    /// </summary>
    /// <param name="salt"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string Encryption512(string salt,string password)
    {
        var str = $"nextstar_{salt}_{password}";
        using var sha = SHA512.Create();
        var bytes = Encoding.Default.GetBytes(str);
        
        var output = Convert.ToBase64String(sha.ComputeHash(bytes));
        return output;
    }
}