using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextStar.ManageService.Core.Businesses
{
    public interface IApplicationConfigBusiness
    {
        Task<List<NextStarAccountDbModels.ApplicationConfig>> GetListAsync();
    }
}