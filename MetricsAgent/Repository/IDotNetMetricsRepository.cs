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
    public interface IDotNetMetricsRepository : IRepositpry<DotNetMetrics>
    {

    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(DotNetMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });

        }

        public IList<DotNetMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<DotNetMetrics>("SELECT * FROM dotnetmetrics WHERE (time >= @from) AND (time =< @to)",
            new
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();

        }
    }
}