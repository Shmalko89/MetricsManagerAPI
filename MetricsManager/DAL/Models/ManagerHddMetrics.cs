using System;
using System.Runtime.InteropServices;


namespace MetricsManager.DAL.Models
{
    public class ManagerHddMetrics
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
