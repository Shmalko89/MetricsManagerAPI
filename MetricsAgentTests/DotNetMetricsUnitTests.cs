using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;

        public DotNetMetricsControllerUnitTests()
        {
            controller = new DotNetMetricsController();
        }
        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetrics(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}
