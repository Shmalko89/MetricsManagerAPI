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
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> logger;
        private IMapper mapper;

        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object, mapper);

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

            mock.Setup(repository => repository.Create(It.IsAny<RamMetrics>())).Verifiable();

            var result = controller.Create(new MetricsAgent.MetricsRequest.RamMetricsCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<RamMetrics>()), Times.AtMostOnce());
        }


    }
}
