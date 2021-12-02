using System.Security.Cryptography;
using System.Text;

namespace NextStar.Library.Core.Abstractions;

public record PassWord
{
    private readonly string _str;
    public PassWord(string str)
    {
        _str = str;
    }

    public override string ToString()
    {
        var bytes = Encoding.UTF8.GetBytes(_str);
        var shaM = new SHA512Managed();
        var result = shaM.ComputeHash(bytes);
        return Encoding.UTF8.GetString(result);
    }
}