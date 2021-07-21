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
    public class RamMetricsControllerUnitTests
    {
        private Mock<ILogger<RamMetricsController>> logger;
        private RamMetricsController controller;
        private Mock<ManagerRamMetricsRepository> mock;
        private IMapper mapper;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<ManagerRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object, mapper);
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