using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }

    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}