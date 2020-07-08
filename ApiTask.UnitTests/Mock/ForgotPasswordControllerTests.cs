using ApiTask.Controllers;
using ApiTask.Model;
using ApiTask.Repository;
using Microsoft.AspNetCore.Http;
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
    public class ForgotPasswordControllerTests
    {

        [Test]
        public async Task PasswordResetEmail_PasswordResetLinkSent_ReturnsOk()
        {
            string email = "fake_email";
            string resetCode = Guid.NewGuid().ToString();

            var registration = new Mock<IRegistration>();
            var httpContext = new DefaultHttpContext();

            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.ToString(),
                Path = $"/user/ResetPassword/{resetCode}"
            };

            var link = uriBuilder.ToString();

            registration.Setup(register => register.SendPasswordResetLinkEmail(email,link)).ReturnsAsync("Password reset link has been sent to your email...");

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var forgotPasswordController = new ForgotPasswordController(registration.Object) 
            {
                ControllerContext = controllerContext,
            };

            var result = await forgotPasswordController.PasswordResetEmail(email);
            var okResult = new OkObjectResult(result);

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task PasswordResetEmail_PasswordResetLinkNotSent_ReturnsNotFound()
        {
            string email = "fake_email";
            string resetCode = Guid.NewGuid().ToString();

            var registration = new Mock<IRegistration>();
            var httpContext = new DefaultHttpContext();

            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.ToString(),
                Path = $"/user/ResetPassword/{resetCode}"
            };

            var link = uriBuilder.ToString();

            registration.Setup(register => register.SendPasswordResetLinkEmail(email, link)).ReturnsAsync("User not found...");

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var forgotPasswordController = new ForgotPasswordController(registration.Object)
            {
                ControllerContext = controllerContext,
            };

            var result = await forgotPasswordController.PasswordResetEmail(email);
            var notFoundResult = new NotFoundObjectResult(result);

            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task ResetPassword_UserResetsPassword_ReturnsOk()
        {
            var registration = new Mock<IRegistration>();
            registration.Setup(register => register.ResetUserPassword(new ResetPassword())).ReturnsAsync("New password updated successfully...");

            var forgotPasswordController = new ForgotPasswordController(registration.Object);

            var result = await forgotPasswordController.ResetPassword(new ResetPassword());
            var okResult = new OkObjectResult(result);

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task ResetPassword_UserUnableToResetPassword_ReturnsNotFound()
        {
            var registration = new Mock<IRegistration>();
            registration.Setup(register => register.ResetUserPassword(new ResetPassword())).ReturnsAsync("Account doesn't exist...");

            var forgotPasswordController = new ForgotPasswordController(registration.Object);

            var result = await forgotPasswordController.ResetPassword(new ResetPassword());
            var notFoundResult = new NotFoundObjectResult(result);

            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

    }
}
