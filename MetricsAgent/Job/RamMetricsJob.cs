using MetricsAgent.DAL;
using MetricsAgent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MetricsAgent.MetricsTable;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ram = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RamMetrics { Time = time, Value = ram });

            return Task.CompletedTask;
        }
    }
}