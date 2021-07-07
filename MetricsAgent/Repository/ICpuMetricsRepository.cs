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
    public interface ICpuMetricsRepository : IRepositpry<CpuMetrics>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public void Create(CpuMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
            
        }

        public IList<CpuMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();
            return connection.Query<CpuMetrics>("SELECT * FROM cpumetrics WHERE (time >= @from) AND (time =< @to)",
            new 
            {
                from = from.ToUnixTimeSeconds(),
                to = to.ToUnixTimeSeconds()
            }).ToList();

        }
    }
}
