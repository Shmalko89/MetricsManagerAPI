using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.Repository;
using MetricsAgent.MetricsTable;
using MetricsAgent.MetricsRequest;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository repository;
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog ������� � HddMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}/left")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("������! ��� ���� ������ ��������� � ���");
            repository.GetByTimePeriod(fromTime, toTime);
            return Ok();
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricsCreateRequest request)
        {
            repository.Create(new HddMetrics
            {
                Time = request.Time,
                Value = request.Value
            });
            return Ok();
        }
    }
}
