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
    public class NetworkMetricsControllerUnitTests
    {
        private Mock<ILogger<NetworkMetricsController>> logger;
        private NetworkMetricsController controller;
        private Mock<ManagerNetworkMetricsRepository> mock;
        private IMapper mapper;

        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<ManagerNetworkMetricsRepository>();
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object, mock.Object, mapper);
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
