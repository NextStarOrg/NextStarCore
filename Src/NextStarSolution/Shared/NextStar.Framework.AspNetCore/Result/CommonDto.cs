using System.Collections.Generic;
using NextStar.Framework.Abstractions.Result;

namespace NextStar.Framework.AspNetCore.Result
{
    public partial class CommonDto<T>:ICommonDto<T>
    {
        public CommonDto()
        {
            
        }
        
        public CommonDto(T data)
        {
            Data = data;
            Success = true;
            ErrorCode = "200";
        }
        
        public CommonDto(Dictionary<string, List<string>> errorList)
        {
            Data = default(T);
            ErrorList = errorList;
            Success = false;
            ErrorCode = "800";
        }
        
        
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, List<string>> ErrorList { get; set; }
    }
}