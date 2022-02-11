using System.Linq.Expressions;

namespace NextStar.BlogService.Core.Repositories.CodeEnvironment;

public interface ICodeEnvironmentRepository
{
    IQueryable<BlogDbModels.CodeEnvironment> GetListQuery(Expression<Func<BlogDbModels.CodeEnvironment, bool>>? searchWhere);
    Task AddEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment);
    Task UpdateEntityAsync(BlogDbModels.CodeEnvironment codeEnvironment);
    Task DeleteEntityAsync(Guid codeEnvironmentKey);
}