using System.Runtime.InteropServices;
using System;

namespace MetricsAgent.MetricsTable
{
    public class HddMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}