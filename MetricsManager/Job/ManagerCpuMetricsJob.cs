using MetricsManager.DAL.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MetricsManager.DAL.Models;
using MetricsManager.Client;
using AutoMapper;
using MetricsManager.Request;

namespace MetricsAgent.Jobs
{
    public class ManagerCpuMetricJob : IJob
    {
        private ManagerCpuMetricsRepository _repository;
        private IMetricsAgentClient _agentclient;
        private IAgentRepository _agentRepository;
        private IMapper _mapper;
        private AgentInfo _agentInfo;

        public ManagerCpuMetricJob(ManagerCpuMetricsRepository repository, IMetricsAgentClient agentClient, IAgentRepository agentRepository, IMapper mapper, AgentInfo agentInfo)
        {
            _repository = repository;
            _agentclient = agentClient;
            _agentRepository = agentRepository;
            _mapper = mapper;
            _agentInfo = agentInfo;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var startTime = _repository.CpuMetricsMaxDate();
            var lasttime = DateTimeOffset.UtcNow;
            var Url = _agentInfo.AgentAddress;


            return Task.CompletedTask;
        }
    }
}
