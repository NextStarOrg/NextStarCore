using Microsoft.EntityFrameworkCore;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;
using NextStar.SystemService.Core.Entities.ApplicationConfig;
using NextStar.SystemService.Core.Repositories.ApplicationConfig;

namespace NextStar.SystemService.Core.Businesses.ApplicationConfig;

public class ApplicationConfigConfigBusiness : IApplicationConfigBusiness
{
    private readonly IApplicationConfigRepository _Repository;
    public ApplicationConfigConfigBusiness(IApplicationConfigRepository Repository)
    {
        _Repository = Repository;
    }

    public async Task<PageCommonDto<ManagementDbModels.ApplicationConfig>> GetApplicationConfigListAsync(SelectInput selectInput)
    {
        var query = _Repository.GetAllQuery();

        if (!string.IsNullOrWhiteSpace(selectInput.SearchText))
        {
            query = query.Where(x => x.Name.Contains(selectInput.SearchText) || x.Value.Contains(selectInput.SearchText));
        }

        query = query.CommonPageSort(selectInput, "Id asc");

        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<ManagementDbModels.ApplicationConfig>()
        {
            Data = result,
            TotalCount = count
        };
    }
}