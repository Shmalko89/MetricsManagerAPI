using System.Runtime.InteropServices;
using System;

namespace MetricsAgent.MetricsTable
{
    public class RamMetrics
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}