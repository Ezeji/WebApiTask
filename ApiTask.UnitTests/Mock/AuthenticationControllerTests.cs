using ApiTask.Controllers;
using ApiTask.Model;
using ApiTask.Repository;
using ApiTask.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiTask.UnitTests.Mock
{
    [TestFixture]
    public class AuthenticationControllerTests
    {

        [Test]
        public async Task LogIn_UserIsRegistered_ReturnsOk()
        {
            var login = new Mock<ILogin>();
            var authenticationService = new Mock<IAuthenticationService>();
            login.Setup(authenticate => authenticate.LoginUser(new LoginUsers())).ReturnsAsync(true);

            var authenticationController = new AuthenticationController(login.Object, authenticationService.Object);

            var result = await authenticationController.LogIn(new LoginUsers());
            var okResult = new OkObjectResult(result);

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task LogIn_UserNotRegistered_ReturnsNotFound()
        {

            var login = new Mock<ILogin>();
            var authenticationService = new Mock<IAuthenticationService>();
            login.Setup(authenticate => authenticate.LoginUser(new LoginUsers())).ReturnsAsync(false);

            var authenticationController = new AuthenticationController(login.Object, authenticationService.Object);

            var result = await authenticationController.LogIn(new LoginUsers());
            var notFoundResult = new NotFoundObjectResult(result);

            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

    }
}
