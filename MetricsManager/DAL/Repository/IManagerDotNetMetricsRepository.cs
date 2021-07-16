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
    public interface IManagerDotNetMetricsRepository : IRepository<ManagerDotNetMetrics>
    {

    }

    public class ManagerDotNetMetricsRepository : IManagerDotNetMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public ManagerDotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(ManagerDotNetMetrics item)
        {
            var connection = _manager.CreateOpenConnection();
            connection.Execute("INSERT INTO managerdotnetmetrics(agentId, value, time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<ManagerDotNetMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerDotNetMetrics>("SELECT * FROM managerdotnetmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();
        }

        public IList<ManagerDotNetMetrics> GetByTimePeriodAgent(int agentId, DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerDotNetMetrics>("SELECT * FROM managerdotnetmetrics WHERE (time >= @from) AND (time =< @to) AND (agentId = @AgentId)",
            new
            {
                AgentId = agentId,
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()

            }).ToList();
        }

        public DateTimeOffset DotNetMetricsMaxDate()
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.ExecuteScalar<DateTimeOffset>("SELECT Max(time) from managerdotnetmetrics");
        }
    }
}