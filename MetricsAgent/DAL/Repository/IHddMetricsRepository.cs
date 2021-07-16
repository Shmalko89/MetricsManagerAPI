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
    public interface IHddMetricsRepository : IRepository<HddMetrics>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(HddMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });

        }

        public IList<HddMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<HddMetrics>("SELECT * FROM hddmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();

        }
    }
}