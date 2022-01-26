namespace NextStar.Library.AspNetCore.Abstractions;

[Serializable]
public class AuditLogActionInfo
{
    public string ServiceName { get; set; } = string.Empty;

    public string MethodName { get; set; } = string.Empty;

    public IDictionary<string, object?> Parameters { get; set; } = new Dictionary<string, object?>();

    public DateTime ExecutionTime { get; set; }

    public int? ExecutionDuration { get; set; }

    //public string HttpMethod { get; set; }

    //public int? HttpStatusCode { get; set; }

    //public string Url { get; set; }

    public Exception? Exception { get; set; }
}