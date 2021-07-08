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
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;

        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network", "Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var network = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetrics { Time = time, Value = network });

            return Task.CompletedTask;
        }
    }
}