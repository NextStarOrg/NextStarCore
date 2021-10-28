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
            return await _accountDbContext.ApplicationConfigs.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }
        
        public async Task<ApplicationConfig> GetDetailByIdAsync(int id)
        {
            return await _accountDbContext.ApplicationConfigs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateConfigAsync(ApplicationConfig config)
        {
            var currentConfig = await _accountDbContext.ApplicationConfigs.FirstOrDefaultAsync(x => x.Id == config.Id);
            if (currentConfig != null)
            {
                currentConfig.Value = config.Value;
                currentConfig.Memo = config.Memo;
                _accountDbContext.ApplicationConfigs.Update(currentConfig);
                await _accountDbContext.SaveChangesAsync();
            }
        }
    }
}