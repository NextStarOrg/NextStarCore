namespace NextStar.Library.AspNetCore.Abstractions;

public interface IApplicationConfigStore
{
    Task<string> GetConfigValueAsync(string name);
    string GetConfigValue(string name);
}