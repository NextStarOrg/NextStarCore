namespace NextStar.Library.MicroService.Outputs;

public partial class CommonDto<T>:ICommonDto<T>
{
    public T? Data { get; set; } = default(T);
    public string ErrorCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public IDictionary<string, string>? ErrorInfo { get; set; } = new Dictionary<string, string>();

    public bool Success => string.IsNullOrWhiteSpace(ErrorCode) && string.IsNullOrWhiteSpace(ErrorMessage) && ErrorInfo?.Count == 0;

    public CommonDto(T? data)
    {
        Data = data;
    }

    public CommonDto(string errorCode)
    {
        ErrorCode = errorCode;
        Data = default(T);
    }

    public CommonDto(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        Data = default(T);
        ErrorMessage = errorMessage;
    }
}