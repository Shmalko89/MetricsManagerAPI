using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }

    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}
