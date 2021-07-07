using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.MetricsTable;

namespace MetricsAgent.Repository
{
    public interface IHddMetricsRepository : IRepositpry<HddMetrics>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public void Create(HddMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<HddMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM hddmetrics WHERE (time >= @from) AND (time =< @to)";

            var returnList = new List<HddMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }
    }
}