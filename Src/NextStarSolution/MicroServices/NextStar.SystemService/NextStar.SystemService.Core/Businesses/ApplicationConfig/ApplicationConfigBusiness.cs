using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;
using NextStar.SystemService.Core.Entities.ApplicationConfig;
using NextStar.SystemService.Core.Repositories.ApplicationConfig;

namespace NextStar.SystemService.Core.Businesses.ApplicationConfig;

public class ApplicationConfigConfigBusiness : IApplicationConfigBusiness
{
    private readonly IApplicationConfigRepository _repository;
    private readonly ILogger<ApplicationConfigConfigBusiness> _logger;
    public ApplicationConfigConfigBusiness(IApplicationConfigRepository repository,
        ILogger<ApplicationConfigConfigBusiness> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PageCommonDto<ManagementDbModels.ApplicationConfig>> GetApplicationConfigListAsync(ApplicationConfigSelectInput applicationConfigSelectInput)
    {
        var query = _repository.GetAllQuery();

        if (!string.IsNullOrWhiteSpace(applicationConfigSelectInput.SearchText))
        {
            query = query.Where(x => x.Name.Contains(applicationConfigSelectInput.SearchText) || x.Value.Contains(applicationConfigSelectInput.SearchText));
        }

        query = query.CommonPageSort(applicationConfigSelectInput, "Id asc");

        var result = await query.ToListAsync();
        var count = await query.CountAsync();
        return new PageCommonDto<ManagementDbModels.ApplicationConfig>()
        {
            Data = result,
            TotalCount = count
        };
    }

    public async Task<bool> UpdateApplicationConfigAsync(ManagementDbModels.ApplicationConfig config)
    {
        try
        {
            await _repository.UpdateAsync(config.Name, config.Value);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"ERROR 10-010-030 Update config value error");
            return false;
        }
    }
}