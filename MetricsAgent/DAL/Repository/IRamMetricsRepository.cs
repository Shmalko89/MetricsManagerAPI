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
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(RamMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });

        }

        public IList<RamMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<RamMetrics>("SELECT * FROM rammetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();

        }
    }
}