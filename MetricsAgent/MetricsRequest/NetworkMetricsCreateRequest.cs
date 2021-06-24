using System;

namespace MetricsAgent.MetricsRequest
{
    public class NetworkMetricsCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
