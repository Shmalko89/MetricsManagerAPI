using System;

namespace MetricsAgent.MetricsRequest
{
    public class CpuMetricsCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
