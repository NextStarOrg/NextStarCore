using System;
using System.Threading.Tasks;
using NextStar.Framework.Utils;
using NextStar.IdentityServer.Models;
using NextStar.IdentityServer.Repositories.User;

namespace NextStar.IdentityServer.Businesses
{
    public class AccountBusiness:IAccountBusiness
    {
        private readonly IUserRepository _userRepository;
        public AccountBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid?> LoginAsync(LoginModel model)
        {
            var userProfile = await _userRepository.GetUserProfile(model.LoginName);
            if (userProfile == null)
            {
                return null;
            }

            var currentEncrypt = PasswordUtils.Encrypt(model.LoginPassword, userProfile.Salt.ToString());
            if (currentEncrypt == userProfile.PassWord)
            {
                return userProfile.UserKey;
            }

            return null;
        }
    }
}