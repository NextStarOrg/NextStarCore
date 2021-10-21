namespace NextStar.Framework.Utils
{
    public static class PasswordUtils
    {
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt">小写</param>
        /// <returns>加密后的小写</returns>
        public static string Encrypt(string password,string salt)
        {
            return EncryptionUtils.Sha512Encryption(string.Format("next{0}star{1}", password, salt.ToLower())).ToLower();
        }
    }
}