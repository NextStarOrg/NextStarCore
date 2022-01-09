namespace NextStar.Library.MicroService.Exceptions;

public class InternalServerException:ServiceApplicationException
{
    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    public override string Message => "发生未知错误信息，请求失败。";

    public override string ToString()
    {
        return "发生未知错误信息，请求失败。";
    }
}