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
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter("Dotnet", "Bytes", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var gcheapsize = Convert.ToInt32(_dotnetCounter.NextValue());
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new DotNetMetrics { Time = time, Value = gcheapsize });

            return Task.CompletedTask;
        }
    }
}
