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
    public class DotNetMetricsControllerUnitTests
    {
        private Mock<ILogger<DotNetMetricsController>> logger;
        private DotNetMetricsController controller;
        private Mock<ManagerDotNetMetricsRepository> mock;
        private IMapper mapper;

        public DotNetMetricsControllerUnitTests()
        {
            mock = new Mock<ManagerDotNetMetricsRepository>();
            logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(logger.Object, mock.Object, mapper);
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

