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

                try
                {
                    await _configCache.SetAsync(NormalizeKey(name),config,new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddHours(8)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Management Application Config Set Cache ({NormalizeKey(name)}) Value Error");
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

                try
                {
                    _configCache.Set(NormalizeKey(name),config,new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddHours(8)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Management Application Config Set Cache ({NormalizeKey(name)}) Value Error");
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

    public async Task<bool> GetConfigBoolAsync(string name)
    {
        var value = await this.GetConfigValueAsync(name);
        if (!string.IsNullOrWhiteSpace(value))
        {
            var isBool = bool.TryParse(value, out var result);
            if (isBool)
            {
                return result;
            }
        }

        return false;
    }

    public bool GetConfigBool(string name)
    {
        var value = this.GetConfigValue(name);
        if (!string.IsNullOrWhiteSpace(value))
        {
            var isBool = bool.TryParse(value, out var result);
            if (isBool)
            {
                return result;
            }
        }

        return false;
    }

    public async Task<int> GetConfigIntAsync(string name)
    {
        var value = await this.GetConfigValueAsync(name);
        if (!string.IsNullOrWhiteSpace(value))
        {
            var isInt = int.TryParse(value, out var result);
            if (isInt)
            {
                return result;
            }
        }

        return 0;
    }

    public int GetConfigInt(string name)
    {
        var value = this.GetConfigValue(name);
        if (!string.IsNullOrWhiteSpace(value))
        {
            var isInt = int.TryParse(value, out var result);
            if (isInt)
            {
                return result;
            }
        }
        return 0;
    }
}