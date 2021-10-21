using System.Security.Cryptography;
using System.Text;

namespace NextStar.Framework.Utils
{
    public static class EncryptionUtils
    {
        /// <summary>
        /// 普通SHA256加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Sha512Encryption(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            return Sha512Core(str);
        }
        
        private static string Sha512Core(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}