using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MetricsAgent.Repository;
using MetricsAgent.MetricsTable;
using Microsoft.Extensions.Logging;
using MetricsAgent.MetricsRequest;


namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> logger;

        public HddMetricsControllerUnitTests()
        {           
            mock = new Mock<IHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object);

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

            mock.Setup(repository => repository.Create(It.IsAny<HddMetrics>())).Verifiable();

            var result = controller.Create(new MetricsAgent.MetricsRequest.HddMetricsCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }


    }
}
