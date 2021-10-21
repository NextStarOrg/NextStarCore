using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Config;
using NextStar.Framework.Core.Consts;
using NextStar.Framework.Utils;

namespace NextStar.Framework.AspNetCore.Config
{
    public class NextStarApplicationConfig : INextStarApplicationConfig
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _settingCache;
        private readonly ILogger<NextStarApplicationConfig> _logger;
        private const int CacheHours = 8;

        public NextStarApplicationConfig(
            IConfiguration configuration,
            IDistributedCache settingCache,
            ILogger<NextStarApplicationConfig> logger)
        {
            _configuration = configuration;
            _settingCache = settingCache;
            _logger = logger;
        }

        private AppSetting _appSettingConfig => _configuration.Get<AppSetting>();

        // PROD DEBUG
        private string _configKey => _appSettingConfig.ConfigKey;

        private string ManagementConnectionString => _appSettingConfig.DataBaseSetting.Account;

        private string NormalizeKey(string key)
        {
            return $"{NextStarCache.NextStar}:applicationconfig:{key}_{_configKey}";
        }

        public async Task<string> GetConfigValueAsync(string name)
        {
            string resultValue = string.Empty;
            try
            {
                resultValue = await _settingCache.GetStringAsync(NormalizeKey(name));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Management Application Config Get ({NormalizeKey(name)}) Value Error");
            }

            if (string.IsNullOrWhiteSpace(resultValue))
            {
                //不存在则需要去数据库中读取
                string sql = "SELECT Value FROM ApplicationConfig WHERE ConfigKey = @ConfigKey AND Name = @Name";
                var dbValue = SqlClientHelper.ExecuteScalar(
                    ManagementConnectionString,
                    CommandType.Text,
                    sql,
                    new SqlParameter("@ConfigKey", _configKey),
                    new SqlParameter("@Name", name));

                if (dbValue != null)
                {
                    try
                    {
                        await _settingCache.SetStringAsync(
                            NormalizeKey(name),
                            dbValue.ToString(),
                            // 防止值出现缓存
                            new DistributedCacheEntryOptions()
                                { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CacheHours) });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e,
                            $"Management Application Config Set ({name}) value ({NormalizeKey(name)}) to cache occur error.");
                    }

                    return dbValue.ToString();
                }

                return "";
            }

            return resultValue;
        }

        public string GetConfigValue(string name)
        {
            string configValue = string.Empty;
            try
            {
                //查看缓存中是否存在
                configValue = _settingCache.GetString(NormalizeKey(name));
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Management Application Config Get ({NormalizeKey(name)}) from cache occur error.");
            }

            if (string.IsNullOrWhiteSpace(configValue))
            {
                //不存在则需要去数据库中读取
                string sql = "SELECT Value FROM ApplicationConfig WHERE ConfigKey = @ConfigKey AND Name = @Name";
                var dbValue = SqlClientHelper.ExecuteScalar(
                    ManagementConnectionString,
                    CommandType.Text,
                    sql,
                    new SqlParameter("@ConfigKey", _configKey),
                    new SqlParameter("@Name", name));

                if (dbValue != null)
                {
                    try
                    {
                        _settingCache.SetString(
                            NormalizeKey(name),
                            dbValue.ToString(),
                            new DistributedCacheEntryOptions()
                                { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CacheHours) });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e,
                            $"Management Application Config Set ({name}) value ({NormalizeKey(name)}) to cache occur error.");
                    }

                    return dbValue.ToString();
                }

                return "";
            }

            return configValue;
        }

        public async Task<int> GetConfigIntValueAsync(string name)
        {
            string resultValue = string.Empty;
            // try
            // {
            //     resultValue = await _settingCache.GetStringAsync(NormalizeKey(name));
            // }
            // catch (Exception e)
            // {
            //     _logger.LogError(e,$"Management Application Config Get ({NormalizeKey(name)}) Value Error");
            // }

            if (string.IsNullOrWhiteSpace(resultValue))
            {
                //不存在则需要去数据库中读取
                string sql = "SELECT Value FROM ApplicationConfig WHERE ConfigKey = @ConfigKey AND Name = @Name";
                var dbValue = SqlClientHelper.ExecuteScalar(
                    ManagementConnectionString,
                    CommandType.Text,
                    sql,
                    new SqlParameter("@ConfigKey", _configKey),
                    new SqlParameter("@Name", name));

                if (dbValue != null)
                {
                    try
                    {
                        await _settingCache.SetStringAsync(
                            NormalizeKey(name),
                            dbValue.ToString(),
                            // 防止值出现缓存
                            new DistributedCacheEntryOptions()
                                { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CacheHours) });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e,
                            $"Management Application Config Set ({name}) value ({NormalizeKey(name)}) to cache occur error.");
                    }

                    resultValue = dbValue.ToString();
                }
            }

            try
            {
                return int.Parse(resultValue);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Management Application Config Get ({name}) value is not int.");
                return 0;
            }
        }

        public int GetConfigIntValue(string name)
        {
            string configValue = string.Empty;
            try
            {
                //查看缓存中是否存在
                configValue = _settingCache.GetString(NormalizeKey(name));
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Management Application Config Get ({NormalizeKey(name)}) from cache occur error.");
            }

            if (string.IsNullOrWhiteSpace(configValue))
            {
                //不存在则需要去数据库中读取
                string sql = "SELECT Value FROM ApplicationConfig WHERE ConfigKey = @ConfigKey AND Name = @Name";
                var dbValue = SqlClientHelper.ExecuteScalar(
                    ManagementConnectionString,
                    CommandType.Text,
                    sql,
                    new SqlParameter("@ConfigKey", _configKey),
                    new SqlParameter("@Name", name));

                if (dbValue != null)
                {
                    try
                    {
                        _settingCache.SetString(
                            NormalizeKey(name),
                            dbValue.ToString(),
                            new DistributedCacheEntryOptions()
                                { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CacheHours) });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e,
                            $"Management Application Config Set ({name}) value ({NormalizeKey(name)}) to cache occur error.");
                    }

                    configValue = dbValue.ToString();
                }
            }

            try
            {
                return int.Parse(configValue);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Management Application Config Get ({name}) value is not int.");
                return 0;
            }
        }
    }
}