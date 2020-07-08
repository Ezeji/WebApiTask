using ApiTask.Controllers;
using ApiTask.Model;
using ApiTask.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ApiTask.UnitTests
{
    [TestFixture]
    public class RegistrationControllerTests
    {

        [Test]
        public async Task SignUp_UserIsRegistered_ReturnsBadRequest()
        {
            var registration = new Mock<IRegistration>();
            registration.Setup(register => register.RegisterUser(new RegisterUsers())).ReturnsAsync(true);

            var registrationController = new RegistrationController(registration.Object);

            var result = await registrationController.SignUp(new RegisterUsers());
            var badResult = new BadRequestObjectResult(result);

            Assert.AreEqual(400, badResult.StatusCode);
        }

        [Test]
        public async Task SignUp_UserNotRegistered_ReturnsOk()
        {
            var registration = new Mock<IRegistration>();
            registration.Setup(register => register.RegisterUser(new RegisterUsers())).ReturnsAsync(true);

            var registrationController = new RegistrationController(registration.Object);

            var result = await registrationController.SignUp(new RegisterUsers());
            var okResult = new OkObjectResult(result);

            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}