using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestingControllers.Controllers;
using TestingControllers.Models;
using TestingControllers.Repository;
using TestingControllers.ViewModels;
using Xunit;

namespace TestingControllersTest
{
    public class HomeControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithListOfSessions()
        {
            // Arrange
            var mockRepo = new Mock<ISessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new HomeController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SessionViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            var mockRepo = new Mock<ISessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new HomeController(mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new SessionViewModel() {
                Name = null
            };

            var result = await controller.Index(newSession);
            var badResultRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badResultRequest.Value);
        }

        private List<SessionModel> GetTestSessions()
        {
            var sessions = new List<SessionModel>();
            sessions.Add(new SessionModel()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1,
                Name = "Test one"
            });
            sessions.Add(new SessionModel()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2,
                Name = "Test two"
            });
            return sessions;
        }
    }
}
