using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.MetricsTable;
using Dapper;
using MetricsAgent.Handler;
using System.Linq;

namespace MetricsAgent.Repository
{
    public interface INetworkMetricsRepository : IRepositpry<NetworkMetrics>
    {

    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(NetworkMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });

        }

        public IList<NetworkMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<NetworkMetrics>("SELECT * FROM networkmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();

        }
    }
}
