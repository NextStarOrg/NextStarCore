using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextStar.ManageService.Core.DbContexts;
using NextStar.ManageService.Core.NextStarAccountDbModels;

namespace NextStar.ManageService.Core.Repositories
{
    public class ApplicationConfigRepository:IApplicationConfigRepository
    {
        private readonly AccountDbContext _accountDbContext;
        public ApplicationConfigRepository(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<List<ApplicationConfig>> GetAllListAsync()
        {
            return await _accountDbContext.ApplicationConfigs.OrderBy(x => x.Id).ToListAsync();
        }
    }
}