using System.Threading.Tasks;

namespace NextStar.Framework.Abstractions.Config
{
    public interface INextStarApplicationConfig
    {
        /// <summary>
        /// 异步获取ApplicationConfig的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<string> GetConfigValueAsync(string name);
        string GetConfigValue(string name);

        Task<int> GetConfigIntValueAsync(string name);
        
        int GetConfigIntValue(string name);
    }
}