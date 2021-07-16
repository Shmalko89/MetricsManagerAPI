using MetricsManager.Controllers;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repository
{
    public interface IAgentRepository
    {
        void RegisterAgent(AgentInfo item);

        void EnableById(int agentId);

        void DisableById(int agentId);

        IList<AgentInfo> GetRegisteredList();
    }
}