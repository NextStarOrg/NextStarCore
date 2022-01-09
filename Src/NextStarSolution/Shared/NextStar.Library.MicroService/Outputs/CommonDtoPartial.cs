namespace NextStar.Library.MicroService.Outputs;

public partial class CommonDto<T>:ICommonDto<T>
{
    public static ICommonDto<T> Ok(T data)
    {
        return new CommonDto<T>(data);
    }
}