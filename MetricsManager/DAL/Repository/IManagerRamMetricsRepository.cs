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
    public interface IManagerRamMetricsRepository : IRepository<ManagerRamMetrics>
    {

    }

    public class ManagerRamMetricsRepository : IManagerRamMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public ManagerRamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(ManagerRamMetrics item)
        {
            var connection = _manager.CreateOpenConnection();
            connection.Execute("INSERT INTO managerrammetrics(agentId, value, time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<ManagerRamMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerRamMetrics>("SELECT * FROM managerrammetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();
        }

        public IList<ManagerRamMetrics> GetByTimePeriodAgent(int agentId, DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerRamMetrics>("SELECT * FROM managerrammetrics WHERE (time >= @from) AND (time =< @to) AND (agentId = @AgentId)",
            new
            {
                AgentId = agentId,
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()

            }).ToList();
        }

        public DateTimeOffset RamMetricsMaxDate()
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.ExecuteScalar<DateTimeOffset>("SELECT Max(time) from managerrammetrics");
        }
    }
}
