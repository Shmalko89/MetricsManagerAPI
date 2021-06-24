using System;

namespace MetricsAgent.MetricsRequest
{
    public class RamMetricsCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}