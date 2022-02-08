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
    public static string Encryption512(Guid salt,string password)
    {
        var str = $"next{salt}star{password}";
        using var sha = SHA512.Create();
        var bytes = Encoding.Default.GetBytes(str);
        var shaStr = sha.ComputeHash(bytes);
        
        var hex = "";
        foreach (byte x in shaStr)
        {
            hex += $"{x:x2}";
        }
        
        var output = Convert.ToBase64String(Encoding.Default.GetBytes(hex));
        return output;
    }
}