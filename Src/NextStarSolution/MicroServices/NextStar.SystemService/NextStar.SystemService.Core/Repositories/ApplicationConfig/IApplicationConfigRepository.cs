namespace NextStar.SystemService.Core.Repositories.ApplicationConfig;

public interface IApplicationConfigRepository
{
    IQueryable<ManagementDbModels.ApplicationConfig> GetAllQuery();
    Task UpdateAsync(string name, string value);
}