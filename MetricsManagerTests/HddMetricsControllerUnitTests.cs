using System;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsManager.DAL.Repository;
using AutoMapper;


namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private Mock<ILogger<HddMetricsController>> logger;
        private HddMetricsController controller;
        private Mock<ManagerHddMetricsRepository> mock;
        private IMapper mapper;

        public HddMetricsControllerUnitTests()
        {
            mock = new Mock<ManagerHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object, mapper);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;

            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {

            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;

            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
