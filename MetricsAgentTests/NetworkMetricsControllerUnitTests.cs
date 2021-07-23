using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MetricsAgent.Repository;
using MetricsAgent.MetricsTable;
using Microsoft.Extensions.Logging;
using MetricsAgent.MetricsRequest;
using AutoMapper;


namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> logger;
        private IMapper mapper;

        public NetworkMetricsControllerUnitTests()
        {
            mock = new Mock<INetworkMetricsRepository>();
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object, mock.Object, mapper);

        }
        [Fact]
        public void GetMetrics_ReturnsOk()
        {

            var fromTime = DateTimeOffset.Now;
            var toTime = DateTimeOffset.Now;

            mock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime));

            var result = controller.GetMetrics(fromTime, toTime);

            mock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime));
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {

            mock.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();

            var result = controller.Create(new MetricsAgent.MetricsRequest.NetworkMetricsCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }


    }
}
