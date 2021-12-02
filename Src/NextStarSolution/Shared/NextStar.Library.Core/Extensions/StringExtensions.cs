using System.Security.Cryptography;
using System.Text;

namespace NextStar.Library.Core.Extensions;

public static class StringExtensions
{
    public static string Sha512(this string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        var shaM = new SHA512Managed();
        var result = shaM.ComputeHash(bytes);
        return Encoding.UTF8.GetString(result);
    }
}