using Microsoft.EntityFrameworkCore;
using NextStar.SystemService.Core.DbContexts;

namespace NextStar.SystemService.Core.Repositories.ApplicationConfig;

public class ApplicationConfigRepository:IApplicationConfigRepository
{
    private readonly ManagementDbContext _managementDbContext;
    public ApplicationConfigRepository(ManagementDbContext managementDbContext)
    {
        _managementDbContext = managementDbContext;
    }

    public IQueryable<ManagementDbModels.ApplicationConfig> GetAllQuery()
    {
        return _managementDbContext.ApplicationConfigs.AsNoTracking();
    }
}