using System;

namespace MetricsAgent.MetricsRequest
{
    public class DotNetMetricsCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
