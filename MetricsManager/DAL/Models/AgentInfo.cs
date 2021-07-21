using System;


   namespace MetricsManager.DAL.Models
{
    public class AgentInfo
    {
        public int AgentId { get; }
        public Uri AgentAddress { get; }
        public bool IsEnabled { get; set; }

    }
}

