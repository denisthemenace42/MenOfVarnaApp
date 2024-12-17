using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Men_Of_Varna.Areas.Admin.Controllers;
using Men_Of_Varna.Areas.Admin.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Tests.UnitTests.Controllers
{
    public class UserManagementControllerTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly UserManagementController _controller;

        public UserManagementControllerTests()
        {
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null
            );

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                null, null, null, null
            );

            _controller = new UserManagementController(_userManagerMock.Object, _roleManagerMock.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            _controller.TempData = new TempDataDictionary(_controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
        }

        [Fact]
        public async Task Index_ShouldReturnViewWithUserViewModel()
        {
            var users = new List<IdentityUser>
        {
            new IdentityUser { Id = "1", Email = "user1@example.com" },
            new IdentityUser { Id = "2", Email = "user2@example.com" }
        };

            _userManagerMock.Setup(x => x.Users).Returns(users.AsQueryable());
            _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<UserViewModel>;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
            Assert.Equal("user1@example.com", model[0].Email);
        }

        [Fact]
        public async Task AssignRole_ShouldAssignRoleToUser()
        {
            var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Admin")).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.AddToRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

            var result = await _controller.AssignRole("1", "Admin");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.AddToRoleAsync(user, "Admin"), Times.Once);
        }

        [Fact]
        public async Task AssignRole_ShouldNotAssignRoleIfUserDoesNotExist()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync("999")).ReturnsAsync((IdentityUser)null);

            var result = await _controller.AssignRole("999", "Admin");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), "Admin"), Times.Never);
        }

        [Fact]
        public async Task RemoveRole_ShouldRemoveRoleFromUser()
        {
            var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Admin")).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.RemoveFromRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

            var result = await _controller.RemoveRole("1", "Admin");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.RemoveFromRoleAsync(user, "Admin"), Times.Once);
        }

        [Fact]
        public async Task RemoveRole_ShouldNotRemoveRoleIfUserDoesNotExist()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync("999")).ReturnsAsync((IdentityUser)null);

            var result = await _controller.RemoveRole("999", "Admin");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.RemoveFromRoleAsync(It.IsAny<IdentityUser>(), "Admin"), Times.Never);
        }

        [Fact]
        public async Task DeleteUser_ShouldDeleteUserIfExists()
        {
            var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _controller.DeleteUser("1");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_ShouldNotDeleteUserIfUserDoesNotExist()
        {
            _userManagerMock.Setup(x => x.FindByIdAsync("999")).ReturnsAsync((IdentityUser)null);

            var result = await _controller.DeleteUser("999");

            Assert.IsType<RedirectToActionResult>(result);
            _userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<IdentityUser>()), Times.Never);
        }

        [Fact]
        public async Task DeleteUser_ShouldRedirectWithErrorMessageIfUserIdIsEmpty()
        {
            var result = await _controller.DeleteUser("");

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("User ID is required.", _controller.TempData["ErrorMessage"]);
        }

        [Fact]
        public async Task DeleteUser_ShouldSetErrorMessageIfDeleteFails()
        {
            var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Failed());

            var result = await _controller.DeleteUser("1");

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Failed to delete user.", _controller.TempData["ErrorMessage"]);
        }
    }
}
