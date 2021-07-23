using System;

namespace MetricsManager.Request
{
    public class AllDotNetMetricsApiRequest
    {
        public Uri AgentUrl { get; set; }
        public DateTimeOffset from { get; set; }
        public DateTimeOffset to { get; set; }
    }
}