using System;
using Xunit;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
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
