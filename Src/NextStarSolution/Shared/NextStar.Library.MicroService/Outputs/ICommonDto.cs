namespace NextStar.Library.MicroService.Outputs;

public interface ICommonDto<T>
{
    public T? Data { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public IDictionary<string, string>? ErrorInfo { get; set; }

    public bool Success { get; }
}