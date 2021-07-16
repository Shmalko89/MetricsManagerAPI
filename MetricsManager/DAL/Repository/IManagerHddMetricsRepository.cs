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
    public interface IManagerHddMetricsRepository : IRepository<ManagerHddMetrics>
    {

    }

    public class ManagerHddMetricsRepository : IManagerHddMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public ManagerHddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(ManagerHddMetrics item)
        {
            var connection = _manager.CreateOpenConnection();
            connection.Execute("INSERT INTO managerhddmetrics(agentId, value, time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<ManagerHddMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerHddMetrics>("SELECT * FROM managerhddmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();
        }

        public IList<ManagerHddMetrics> GetByTimePeriodAgent(int agentId, DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerHddMetrics>("SELECT * FROM managerhddmetrics WHERE (time >= @from) AND (time =< @to) AND (agentId = @AgentId)",
            new
            {
                AgentId = agentId,
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()

            }).ToList();
        }

        public DateTimeOffset HddMetricsMaxDate()
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.ExecuteScalar<DateTimeOffset>("SELECT Max(time) from managerhddmetrics");
        }
    }
}