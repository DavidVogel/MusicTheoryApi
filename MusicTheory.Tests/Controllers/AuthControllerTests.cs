using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using MusicTheory.Domain;
using MusicTheory.Api.Models;
using MusicTheory.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace MusicTheory.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_ValidModel_ReturnsCreated()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null
            );
            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var mockConfig = new Mock<IConfiguration>();
            var controller = new AuthController(mockUserManager.Object, mockConfig.Object);

            var model = new RegisterDto { Email = @"test@example.com", Password = @"Password123" };

            // Act
            var result = await controller.Register(model);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Null(createdResult.Value);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null
            );
            mockUserManager.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(default(ApplicationUser)); // User not found

            var mockConfig = new Mock<IConfiguration>();
            var controller = new AuthController(mockUserManager.Object, mockConfig.Object);

            var model = new LoginDto { Email = @"test@example.com", Password = @"Password123" };

            // Act
            var result = await controller.Login(model);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Contains(@"Invalid credentials", unauthorizedResult.Value.ToString());
        }
    }
}
