using System.Linq.Expressions;

namespace NextStar.SystemService.Core.Repositories.ApplicationConfig;

public interface IApplicationConfigRepository
{
    IQueryable<ManagementDbModels.ApplicationConfig> GetAllQuery(string searchText,Expression<Func<ManagementDbModels.ApplicationConfig,bool>> predicate);
    Task UpdateAsync(string name, string value);
}