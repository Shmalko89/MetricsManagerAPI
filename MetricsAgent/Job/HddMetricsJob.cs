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
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;

        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("Disk", "Avalible space", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddSpace = Convert.ToInt32(_hddCounter.NextValue());
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new HddMetrics { Time = time, Value = hddSpace });

            return Task.CompletedTask;
        }
    }
}