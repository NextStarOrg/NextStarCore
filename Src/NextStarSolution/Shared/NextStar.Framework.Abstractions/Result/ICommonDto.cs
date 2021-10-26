using System.Collections.Generic;

namespace NextStar.Framework.Abstractions.Result
{
    public interface ICommonDto<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        string ErrorCode { get; set; }
        Dictionary<string,List<string>> ErrorList { get; set; }
    }
}