using System;

namespace MetricsManager.Request
{
    public class AllRamMetricsApiRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset from { get; set; }
        public DateTimeOffset to { get; set; }
    }
}