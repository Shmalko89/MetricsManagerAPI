using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.Repository;
using MetricsAgent.Handler;

namespace MetricsManager.DAL.Repository
{
    public interface IManagerNetworkMetricsRepository : IRepository<ManagerNetworkMetrics>
    {

    }

    public class ManagerNetworkMetricsRepository : IManagerNetworkMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public ManagerNetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(ManagerNetworkMetrics item)
        {
            var connection = _manager.CreateOpenConnection();
            connection.Execute("INSERT INTO managernetworkmetrics(agentId, value, time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<ManagerNetworkMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerNetworkMetrics>("SELECT * FROM managernetworkmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();
        }

        public IList<ManagerNetworkMetrics> GetByTimePeriodAgent(int agentId, DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerNetworkMetrics>("SELECT * FROM managernetworkmetrics WHERE (time >= @from) AND (time =< @to) AND (agentId = @AgentId)",
            new
            {
                AgentId = agentId,
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()

            }).ToList();
        }

        public DateTimeOffset NetworkMetricsMaxDate()
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.ExecuteScalar<DateTimeOffset>("SELECT Max(time) from managernetworkmetrics");
        }
    }
}
