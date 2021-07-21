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
    public interface IManagerCpuMetricsRepository : IRepository<ManagerCpuMetrics>
    {
        
    }

    public class ManagerCpuMetricsRepository : IManagerCpuMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public ManagerCpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(ManagerCpuMetrics item)
        {
            var connection = _manager.CreateOpenConnection();
            connection.Execute("INSERT INTO managercpumetrics(agentId, value, time) VALUES (@AgentId, @Value, @Time)",
                new
                {
                    item.AgentId,
                    item.Value,
                    item.Time
                });
        }

        public IList<ManagerCpuMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerCpuMetrics>("SELECT * FROM managercpumetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();
        }

        public IList<ManagerCpuMetrics> GetByTimePeriodAgent(int agentId, DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<ManagerCpuMetrics>("SELECT * FROM managercpumetrics WHERE (time >= @from) AND (time =< @to) AND (agentId = @AgentId)",
            new
            {
                AgentId = agentId,
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()

            }).ToList();
        }

        public DateTimeOffset CpuMetricsMaxDate()
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.ExecuteScalar<DateTimeOffset>("SELECT Max(time) from managercpumetrics");
        }

    }
}