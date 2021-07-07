using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Repository
{
    public class ConnectionManager
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public SQLiteConnection CreateOpenConnection()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.OpenAndReturn();
        }
    }
}
