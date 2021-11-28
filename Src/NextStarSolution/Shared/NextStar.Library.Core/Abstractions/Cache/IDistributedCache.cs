using Microsoft.Extensions.Caching.Distributed;

namespace NextStar.Library.Core.Abstractions;

public interface IDistributedCache<TCacheItem>
{
    TCacheItem? Get(string key);

    Task<TCacheItem?> GetAsync(string key, CancellationToken token = default);

    TCacheItem? GetOrAdd(string key, Func<TCacheItem> factory, DistributedCacheEntryOptions? options = null);

    Task<TCacheItem?> GetOrAddAsync(
        string key,
        Func<Task<TCacheItem>> factory,
        DistributedCacheEntryOptions? options = null,
        CancellationToken token = default
    );

    void Set(
        string key,
        TCacheItem value,
        DistributedCacheEntryOptions? options = null
    );

    Task SetAsync(
        string key,
        TCacheItem value,
        DistributedCacheEntryOptions? options = null,
        CancellationToken token = default
    );

    void Remove(string key);

    Task RemoveAsync(string key, CancellationToken token = default);

    #region Todo Function

    //void SetMany(
    //    IEnumerable<KeyValuePair<string, TCacheItem>> items,
    //    DistributedCacheEntryOptions options = null
    //);

    //Task SetManyAsync(
    //    IEnumerable<KeyValuePair<string, TCacheItem>> items,
    //    DistributedCacheEntryOptions options = null,
    //    CancellationToken token = default
    //);

    //void Refresh(
    //    string key,
    //    bool? hideErrors = null
    //);

    //Task RefreshAsync(
    //    string key,
    //    CancellationToken token = default
    //);

    //KeyValuePair<string, TCacheItem>[] GetMany(IEnumerable<string> keys);

    //Task<KeyValuePair<string, TCacheItem>[]> GetManyAsync(IEnumerable<string> keys, CancellationToken token = default);

    #endregion
}