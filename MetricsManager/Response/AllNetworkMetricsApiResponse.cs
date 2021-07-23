using System;
using System.Collections.Generic;

namespace MetricsManager.Response
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }

    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int AgentId { get; set; }
    }
}
