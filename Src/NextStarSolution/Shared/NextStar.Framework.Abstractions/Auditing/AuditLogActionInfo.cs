using System;
using System.Collections.Generic;

namespace NextStar.Framework.Abstractions.Auditing
{
    [Serializable]
    public class AuditLogActionInfo
    {
        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public IDictionary<string, object> Parameters { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int? ExecutionDuration { get; set; }

        //public string HttpMethod { get; set; }

        //public int? HttpStatusCode { get; set; }

        //public string Url { get; set; }

        public Exception Exception { get; set; }
    }
}