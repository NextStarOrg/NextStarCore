using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.MicroService.Outputs;
using NextStar.Library.MicroService.Utils;
using NextStar.SystemService.Core.Entities.ApplicationConfig;
using NextStar.SystemService.Core.Repositories.ApplicationConfig;

namespace NextStar.SystemService.Core.Businesses.ApplicationConfig;

public class ApplicationConfigConfigBusiness : IApplicationConfigBusiness
{
    private readonly IApplicationConfigRepository _repository;
    private readonly ILogger<ApplicationConfigConfigBusiness> _logger;
    private readonly IApplicationConfigStore _applicationConfigStore;
    public ApplicationConfigConfigBusiness(IApplicationConfigRepository repository,
        ILogger<ApplicationConfigConfigBusiness> logger,
        IApplicationConfigStore applicationConfigStore)
    {
        _repository = repository;
        _logger = logger;
        _applicationConfigStore = applicationConfigStore;
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
            // 更新之后删除掉原有缓存数据 - 之前删除可能导致正在更新时有访问导致缓存依旧为旧版数据
            await _applicationConfigStore.ClearConfigCacheAsync(config.Name);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"ERROR 10-010-030 Update config value error");
            return false;
        }
    }
}