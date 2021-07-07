using System;

namespace MetricsAgent.MetricsRequest
{
    public class HddMetricsCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
