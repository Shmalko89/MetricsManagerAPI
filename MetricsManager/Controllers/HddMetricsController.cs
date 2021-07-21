using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsManager.DAL.Repository;
using MetricsManager.DAL.Models;
using MetricsManager.Response;



namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ManagerHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;
        public HddMetricsController(ILogger<HddMetricsController> logger, ManagerHddMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _mapper = mapper;
        }

        [HttpGet("aagent/{agentId}/from/{fromTime}/to/{toTime}/left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("$Time: from {fromTime} to {toTime}");

            IList<ManagerHddMetrics> metrics = _repository.GetByTimePeriodAgent(agentId, fromTime, toTime);

            var response = new AllHddMetricsApiResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/left")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("$Time: from {fromTime} to {toTime}");

            IList<ManagerHddMetrics> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsApiResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}