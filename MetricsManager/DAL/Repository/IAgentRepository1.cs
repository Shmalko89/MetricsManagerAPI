using MetricsManager.Controllers;
using System.Collections.Generic;

namespace MetricsManager.DAL.Repository
{
    public interface IAgentRepository1
    {
        void DisableById(int agentId);
        void EnableById(int agentId);
        IList<AgentInfo> GetRegisteredList();
        void RegisterAgent(AgentInfo item);
    }
}