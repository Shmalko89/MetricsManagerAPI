using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;


namespace MetricsManager.DAL.Repository
{
    public class AgentsRepository : IAgentRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public void RegisterAgent(AgentInfo item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO agents(AgentUrl, Enabled) VALUES(@agentUrl, @isEnabled)",
            new
            {
                agentUrl = item.AgentAddress,
                isEnabled = item.IsEnabled
            }) ;
        }

        public void EnableById(int agentId)
        {
            ChangeStatusById(agentId, true);
        }

        public void DisableById(int agentId)
        {
            ChangeStatusById(agentId, false);
        }

        public IList<AgentInfo> GetRegisteredList()
        {
            using var connection = _manager.CreateOpenConnection();

            IList<AgentInfo> result = connection.Query<AgentInfo>("SELECT * FROM agents").ToList();

            return result;
        }

        private void ChangeStatusById(int agentId, bool status)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("UPDATE agents SET Enabled = @state WHERE AgentId = @agentId",
                new
                {
                    state = status,
                    AgentId = agentId
                });
        }

    }
}
