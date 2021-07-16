using System.Collections.Generic;
using System;

namespace MetricsAgent.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to);
        void Create (T item);
    }
}

