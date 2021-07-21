using System;

namespace MetricsManager.Request
{
    public class AllNetworkMetricsApiRequest
    {
        public Uri AgentUrl { get; set; }
        public DateTimeOffset from { get; set; }
        public DateTimeOffset to { get; set; }
    }
}