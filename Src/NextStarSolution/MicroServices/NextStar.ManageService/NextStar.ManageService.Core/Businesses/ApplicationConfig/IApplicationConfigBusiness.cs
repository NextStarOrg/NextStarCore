using System.Collections.Generic;
using System.Threading.Tasks;
using NextStar.ManageService.Core.NextStarAccountDbModels;

namespace NextStar.ManageService.Core.Businesses
{
    public interface IApplicationConfigBusiness
    {
        Task<List<NextStarAccountDbModels.ApplicationConfig>> GetListAsync();
        Task<NextStarAccountDbModels.ApplicationConfig> GetDetailByIdAsync(int id);
        Task UpdateConfigAsync(ApplicationConfig config);
    }
}