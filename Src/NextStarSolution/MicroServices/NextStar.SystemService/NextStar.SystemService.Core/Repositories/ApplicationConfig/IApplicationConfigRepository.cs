namespace NextStar.SystemService.Core.Repositories.ApplicationConfig;

public interface IApplicationConfigRepository
{
    IQueryable<ManagementDbModels.ApplicationConfig> GetAllQuery();
}