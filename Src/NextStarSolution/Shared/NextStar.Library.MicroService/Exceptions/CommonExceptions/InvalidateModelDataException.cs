using System.ComponentModel;
using NextStar.Library.MicroService.Consts;
using NextStar.Library.MicroService.Extensions;

namespace NextStar.Library.MicroService.Exceptions;

/// <summary>
/// 模型数据不合法的异常
/// </summary>
public class InvalidateModelDataException : ServiceApplicationException
{
    /// <summary>
    /// 不合法的属性名
    /// </summary>
    public string Property { get; set; } = string.Empty;

    /// <summary>
    /// 不合法的类型
    /// </summary>
    public InvalidateType Type { get; set; } = InvalidateType.UnknownError;

    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    public override string Message => $"信息数据不正确。<br/> {Property} {Type.GetMappingName()}。";

    public override string ToString()
    {
        return $"信息数据是不正确的。<br/> {Property} {Type.ToString()}。";
    }
    
    /// <summary>
    /// 非法数据的类型
    /// </summary>
    /// <remarks></remarks>
    /// <history>
    /// [ValentineNing]   2012/05/07 12:23    Created
    /// </history>
    public enum InvalidateType
    {
        [MappingName("必须项目")]
        Required,
        [MappingName("未知错误")]
        UnknownError
    }
}