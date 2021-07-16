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
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CpuMetricsController>> logger;
        private IMapper mapper;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            logger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(logger.Object, mock.Object, mapper);

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
           
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetrics>())).Verifiable();

            var result = controller.Create(new MetricsAgent.MetricsRequest.CpuMetricsCreateRequest { Time = DateTimeOffset.Now, Value = 50 });

            mock.Verify(repository => repository.Create(It.IsAny<CpuMetrics>()), Times.AtMostOnce());
        }


    }
}

