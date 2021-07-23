using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class DotNetMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}