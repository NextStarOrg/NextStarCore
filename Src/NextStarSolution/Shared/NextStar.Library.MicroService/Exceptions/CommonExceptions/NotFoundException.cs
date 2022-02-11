namespace NextStar.Library.MicroService.Exceptions;

public class NotFoundException:ServiceApplicationException
{
    /// <summary>
    /// 主key或者id
    /// </summary>
    public string Key { get; set; } = string.Empty;
    
}