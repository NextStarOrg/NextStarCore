using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.DbContexts;
using NextStar.Library.AspNetCore.ManagementDbModels;
using NextStar.Library.AspNetCore.SessionDbModels;
using NextStar.Library.Core.Abstractions;

namespace NextStar.Library.AspNetCore.Stores;

public class ApplicationConfigStore:IApplicationConfigStore
{
    private readonly ManagementDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApplicationConfigStore> _logger;
    private readonly IDistributedCache<ApplicationConfig> _configCache;
    
    public ApplicationConfigStore(ManagementDbContext context,
        ILogger<ApplicationConfigStore> logger,
        IDistributedCache<ApplicationConfig> configCache,
        IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configCache = configCache;
        _configuration = configuration;
    } 
    private AppSetting _appSettingConfig => _configuration.Get<AppSetting>();
    private string _configEnvironment => _appSettingConfig.ConfigEnvironment;
    private string NormalizeKey(string key)
    {
        return $"{_configEnvironment}_{key}";
    }
    
    public async Task<string> GetConfigValueAsync(string name)
    {
        ApplicationConfig? config = null;
        try
        {
            config = await _configCache.GetAsync(NormalizeKey(name));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Management Application Config Get ({NormalizeKey(name)}) Value Error");
        }

        if (config == null)
        {
            try
            {
                config = await _context.ApplicationConfigs.FirstOrDefaultAsync(x =>
                    x.Name == name && x.Environment == _configEnvironment);
                if (config == null)
                {
                    return string.Empty;
                }

                return config.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Management Application Config Get ({NormalizeKey(name)}) Value Error");
            }
        }
        else
        {
            return config.Value;
        }
        return string.Empty;
    }

    public string GetConfigValue(string name)
    {
        ApplicationConfig? config = null;
        try
        {
            config = _configCache.Get(NormalizeKey(name));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Management Application Config Get ({NormalizeKey(name)}) Value Error");
        }

        if (config == null)
        {
            try
            {
                config = _context.ApplicationConfigs.FirstOrDefault(x =>
                    x.Name == name && x.Environment == _configEnvironment);
                if (config == null)
                {
                    return string.Empty;
                }

                return config.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Management Application Config Get ({NormalizeKey(name)}) Value Error");
            }
        }
        else
        {
            return config.Value;
        }
        return string.Empty;
    }
}