using System.Runtime.InteropServices;
using System;

namespace MetricsManager.DAL.Models
{
    public class ManagerNetworkMetrics
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}