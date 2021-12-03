using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NextStar.Library.AspNetCore.Abstractions;
using NextStar.Library.AspNetCore.DbContexts;
using NextStar.Library.AspNetCore.ManagementDbModels;
using NextStar.Library.AspNetCore.SessionDbModels;
using NextStar.Library.Core.Abstractions;

namespace NextStar.Library.AspNetCore.Stores;

public class ApplicationConfigStore : IApplicationConfigStore
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

    private AppSetting AppSettingConfig => _configuration.Get<AppSetting>();
    private string ConfigEnvironment => AppSettingConfig.ConfigEnvironment;

    private string NormalizeKey(string key)
    {
        return $"{ConfigEnvironment}_{key}";
    }

    public async Task<string> GetConfigValueAsync(string name)
    {
        var configCacheKey = NormalizeKey(name);
        ApplicationConfig? config = null;
        try
        {
            config = await _configCache.GetAsync(configCacheKey);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Management Application Config Get ({ConfigCacheKey}) Value Error",configCacheKey);
        }

        if (config == null)
        {
            try
            {
                config = await _context.ApplicationConfigs.FirstOrDefaultAsync(x =>
                    x.Name == name && x.Environment == ConfigEnvironment);
                if (config == null)
                {
                    return string.Empty;
                }

                try
                {
                    await _configCache.SetAsync(configCacheKey, config, new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddHours(8)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Management Application Config Set Cache ({ConfigCacheKey}) Value Error",configCacheKey);
                }

                return config.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Management Application Config Get ({ConfigCacheKey}) Value Error",configCacheKey);
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
        var configCacheKey = NormalizeKey(name);
        ApplicationConfig? config = null;
        try
        {
            config = _configCache.Get(configCacheKey);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Management Application Config Get ({ConfigCacheKey}) Value Error",configCacheKey);
        }

        if (config == null)
        {
            try
            {
                config = _context.ApplicationConfigs.FirstOrDefault(x =>
                    x.Name == name && x.Environment == ConfigEnvironment);
                if (config == null)
                {
                    return string.Empty;
                }

                try
                {
                    _configCache.Set(configCacheKey, config, new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddHours(8)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Management Application Config Set Cache ({ConfigCacheKey}) Value Error",configCacheKey);
                }

                return config.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Management Application Config Get ({ConfigCacheKey}) Value Error",configCacheKey);
            }
        }
        else
        {
            return config.Value;
        }

        return string.Empty;
    }

    public async Task ClearConfigCacheAsync(string name)
    {
        var configCacheKey = NormalizeKey(name);
        try
        {
            var config = await _configCache.GetAsync(configCacheKey);
            if (config == null) return;
            await _configCache.RemoveAsync(configCacheKey);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Management Application Config Clear Cache ({ConfigCacheKey}) Value Error",configCacheKey);
        }
    }

    public async Task<bool> GetConfigBoolAsync(string name)
    {
        var value = await this.GetConfigValueAsync(name);
        if (string.IsNullOrWhiteSpace(value)) return false;
        var isBool = bool.TryParse(value, out var result);
        return isBool && result;
    }

    public bool GetConfigBool(string name)
    {
        var value = this.GetConfigValue(name);
        if (string.IsNullOrWhiteSpace(value)) return false;
        var isBool = bool.TryParse(value, out var result);
        return isBool && result;
    }

    public async Task<int> GetConfigIntAsync(string name)
    {
        var value = await this.GetConfigValueAsync(name);
        if (string.IsNullOrWhiteSpace(value)) return 0;
        var isInt = int.TryParse(value, out var result);
        return isInt ? result : 0;
    }

    public int GetConfigInt(string name)
    {
        var value = this.GetConfigValue(name);
        if (string.IsNullOrWhiteSpace(value)) return 0;
        var isInt = int.TryParse(value, out var result);
        return isInt ? result : 0;
    }
}