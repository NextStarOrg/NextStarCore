using System.Text;

namespace NextStar.Library.AspNetCore.Abstractions;

[Serializable]
public class AuditLogInfo
{
    public DateTime ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public string HttpMethod { get; set; } = string.Empty;

    public int? HttpStatusCode { get; set; }

    public string Url { get; set; } = string.Empty;

    public List<AuditLogActionInfo> Actions { get; set; }

    public List<Exception> Exceptions { get; }

    public AuditLogInfo()
    {
        Actions = new List<AuditLogActionInfo>();
        Exceptions = new List<Exception>();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine(
            $"AUDIT LOG: [{HttpStatusCode?.ToString() ?? "---"}: {(HttpMethod ?? "-------").PadRight(7)}] {Url}");
        sb.AppendLine($"- ExecutionDuration      : {ExecutionDuration}");

        if (Actions.Any())
        {
            sb.AppendLine("- Actions:");
            foreach (var action in Actions)
            {
                sb.AppendLine($"  - {action.ServiceName}.{action.MethodName} ({action.ExecutionDuration} ms.)");
                sb.AppendLine($"    {action.Parameters}");
            }
        }

        if (Exceptions.Any())
        {
            sb.AppendLine("- Exceptions:");
            foreach (var exception in Exceptions)
            {
                sb.AppendLine($"  - {exception.Message}");
                sb.AppendLine($"    {exception}");
            }
        }

        return sb.ToString();
    }
}