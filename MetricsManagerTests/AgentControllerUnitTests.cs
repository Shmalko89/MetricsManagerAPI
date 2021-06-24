using System;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private Mock<ILogger<AgentsController>> logger;
        private AgentsController controller;
        

        public AgentsControllerUnitTests()
        {
            logger = new Mock<ILogger<AgentsController>>();
            controller = new AgentsController(logger.Object);  
        }
        [Fact]
        public void RegistrAgent_ReturnsOk()
        {
            var agentinfo = new AgentInfo();
            

            var result = controller.RegisterAgent(agentinfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {

            var AgentId = 1;

            var result = controller.EnableAgentById(AgentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {

            var AgentId = 1;

            var result = controller.DisableAgentById(AgentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
