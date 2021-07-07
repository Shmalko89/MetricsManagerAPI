using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.MetricsTable;

namespace MetricsAgent.Repository
{
    public interface IRamMetricsRepository : IRepositpry<RamMetrics>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly ConnectionManager _manager = new ConnectionManager();

        public void Create(RamMetrics item)
        {
            using var connection = _manager.CreateOpenConnection();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetrics> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = _manager.CreateOpenConnection();

            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "SELECT * FROM rammetrics WHERE (time >= @from) AND (time =< @to)";

            var returnList = new List<RamMetrics>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetrics
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