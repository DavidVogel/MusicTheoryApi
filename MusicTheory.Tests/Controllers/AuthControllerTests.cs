using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using MusicTheory.Domain;
using MusicTheory.Api.Models;
using MusicTheory.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using MusicTheory.Api.Utils;

namespace MusicTheory.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the AuthController class.
    /// </summary>
    public class AuthControllerTests
    {
        /// <summary>
        /// Tests the Register method with a valid model.
        /// </summary>
        [Fact]
        public async Task Register_ValidModel_ReturnsCreated()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null
            );
            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var mockConfig = new Mock<IConfiguration>();
            var controller = new AuthController(mockUserManager.Object, mockConfig.Object);

            var model = new RegisterDto { Email = @"test@example.com", Password = @"Password123" };

            var result = await controller.Register(model);

            var createdResult = Assert.IsType<CreatedResult>(result);
            var userCreatedResponse = Assert.IsType<UserCreatedResponse>(createdResult.Value);
            Assert.Equal("test@example.com created successfully!", userCreatedResponse.Message);
        }

        /// <summary>
        /// Tests the Login method with invalid credentials.
        /// </summary>
        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null
            );
            mockUserManager.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(default(ApplicationUser)); // User not found

            var mockConfig = new Mock<IConfiguration>();
            var controller = new AuthController(mockUserManager.Object, mockConfig.Object);

            var model = new LoginDto { Email = @"test@example.com", Password = @"Password123" };

            var result = await controller.Login(model);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Contains(@"Invalid credentials", unauthorizedResult.Value.ToString());
        }
    }
}
