using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NextStar.Framework.Abstractions.Cache;

namespace NextStar.Framework.AspNetCore.Cache
{
    public class DistributedCache<TCacheItem> : IDistributedCache<TCacheItem>
        where TCacheItem : class
    {
        protected IDistributedCache Cache { get; }
        protected DistributedCacheEntryOptions DefaultCacheOptions;
        protected string CacheName { get; set; }
        protected ILogger Logger;
        protected SemaphoreSlim SyncSemaphore { get; }

        public DistributedCache(IDistributedCache cache, ILogger<DistributedCache<TCacheItem>> logger)
        {
            Cache = cache;
            CacheName = typeof(TCacheItem).Name.ToLower();
            DefaultCacheOptions = new DistributedCacheEntryOptions();
            Logger = logger;
            SyncSemaphore = new SemaphoreSlim(1, 1);
        }

        protected virtual string NormalizeKey(string key)
        {
            return $"nextstar:{CacheName}:{key.ToLower()}";
        }

        public TCacheItem Get(string key)
        {
            string cachedString;
            try
            {
                cachedString = Cache.GetString(NormalizeKey(key));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Get cache item ({CacheKey}) error. ", NormalizeKey(key));
                return null;
                //throw;
            }

            if (string.IsNullOrEmpty(cachedString))
            {
                return null;
            }

            return JsonSerializer.Deserialize<TCacheItem>(cachedString);
        }

        public async Task<TCacheItem> GetAsync(string key, CancellationToken token = default)
        {
            string cachedString;
            try
            {
                cachedString = await Cache.GetStringAsync(NormalizeKey(key));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Get cache item ({CacheKey}) error. ", NormalizeKey(key));
                return null;
            }

            if (string.IsNullOrEmpty(cachedString))
            {
                return null;
            }

            return JsonSerializer.Deserialize<TCacheItem>(cachedString);
        }

        public TCacheItem GetOrAdd(string key, Func<TCacheItem> factory, DistributedCacheEntryOptions options = null)
        {
            var value = Get(key);
            if (value != null)
            {
                return value;
            }

            SyncSemaphore.Wait();
            try
            {
                value = Get(key);
                if (value != null)
                {
                    return value;
                }

                value = factory();
                Set(key, value, options);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Get cache item ({CacheKey}) error. ", NormalizeKey(key));

                return null;
            }
            finally
            {
                SyncSemaphore.Release();
            }

            return value;
        }

        public async Task<TCacheItem> GetOrAddAsync(string key, Func<Task<TCacheItem>> factory,
            DistributedCacheEntryOptions options = null, CancellationToken token = default)
        {
            var value = await GetAsync(key);
            if (value != null)
            {
                return value;
            }

            await SyncSemaphore.WaitAsync();
            try
            {
                value = await GetAsync(key);
                if (value != null)
                {
                    return value;
                }

                value = await factory();
                await SetAsync(key, value, options);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Get cache item ({CacheKey}) error. ", NormalizeKey(key));

                return null;
            }
            finally
            {
                SyncSemaphore.Release();
            }

            return value;
        }


        public void Remove(string key)
        {
            try
            {
                Cache.Remove(NormalizeKey(key));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Remove cache item ({CacheKey}) error. ", NormalizeKey(key));
            }
        }

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            try
            {
                await Cache.RemoveAsync(NormalizeKey(key));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Remove cache item ({CacheKey}) error. ", NormalizeKey(key));
            }
        }

        public void Set(string key, TCacheItem value, DistributedCacheEntryOptions options = null)
        {
            try
            {
                Cache.SetString(
                    NormalizeKey(key),
                    JsonSerializer.Serialize(value),
                    options ?? DefaultCacheOptions
                );
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Set cache item ({CacheKey} - {@CacheItem}) error. ", NormalizeKey(key), value);
            }
        }

        public async Task SetAsync(string key, TCacheItem value, DistributedCacheEntryOptions options = null,
            CancellationToken token = default)
        {
            try
            {
                await Cache.SetStringAsync(
                    NormalizeKey(key),
                    JsonSerializer.Serialize(value),
                    options ?? DefaultCacheOptions
                );
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Set cache item ({CacheKey} - {@CacheItem}) error. ", NormalizeKey(key), value);
            }
        }

        #region Todo Function

        //public void Refresh(string key, bool? hideErrors = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RefreshAsync(string key, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMany(IEnumerable<KeyValuePair<string, TCacheItem>> items, DistributedCacheEntryOptions options = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetManyAsync(IEnumerable<KeyValuePair<string, TCacheItem>> items, DistributedCacheEntryOptions options = null, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}


        //public KeyValuePair<string, TCacheItem>[] GetMany(IEnumerable<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<KeyValuePair<string, TCacheItem>[]> GetManyAsync(IEnumerable<string> keys, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}