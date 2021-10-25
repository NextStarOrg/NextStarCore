using System;
using System.Threading.Tasks;
using NextStar.IdentityServer.Models;

namespace NextStar.IdentityServer.Businesses
{
    public interface IAccountBusiness
    {
        Task<Guid?> LoginAsync(LoginModel model);
    }
}