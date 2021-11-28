namespace NextStar.Library.AspNetCore.Abstractions;

public interface IApplicationConfigStore
{
    Task<string> GetConfigValueAsync(string name);
    string GetConfigValue(string name);
    /// <summary>
    /// 无值返回false，在进行配置数据时请注意
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> GetConfigBoolAsync(string name);
    /// <summary>
    /// 无值返回false，在进行配置数据时请注意
    /// </summary>
    bool GetConfigBool(string name);
    /// <summary>
    /// 无值返回都为0
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<int> GetConfigIntAsync(string name);
    /// <summary>
    /// 无值返回都为0
    /// </summary>
    int GetConfigInt(string name);
}