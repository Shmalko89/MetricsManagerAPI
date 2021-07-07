using System.Runtime.InteropServices;
using System;

namespace MetricsAgent.MetricsTable
{
    public class CpuMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
