using System.Collections.Generic;
using NextStar.Framework.Abstractions.Result;

namespace NextStar.Framework.AspNetCore.Result
{
    public partial class CommonDto<T> : ICommonDto<T>
    {
        /// <summary>
        /// 200 OK
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> OkResult()
        {
            return new CommonDto<T>()
            {
                ErrorCode = string.Empty,
                Success = true
            };
        }

        /// <summary>
        /// 400 Bad Request
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> BadRequestResult()
        {
            return new CommonDto<T>()
            {
                ErrorCode = "400",
                Success = false
            };
        }


        /// <summary>
        /// 403 Forbidden
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> ForbiddenResult()
        {
            return new CommonDto<T>()
            {
                ErrorCode = "403",
                Success = false
            };
        }


        /// <summary>
        /// 404 Not Found
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> NotFoundResult()
        {
            return new CommonDto<T>()
            {
                ErrorCode = "404",
                Success = false
            };
        }


        /// <summary>
        /// 500 Internal Server Error
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> InternalServerErrorResult()
        {
            return new CommonDto<T>()
            {
                ErrorCode = "500",
                Success = false
            };
        }

        /// <summary>
        /// 801 Data Validation ErrorList
        /// </summary>
        /// <returns></returns>
        public static CommonDto<T> DataValidationErrorResult(Dictionary<string, List<string>> errorList = default)
        {
            return new CommonDto<T>()
            {
                ErrorList = errorList,
                ErrorCode = "801",
                Success = false
            };
        }
    }
}