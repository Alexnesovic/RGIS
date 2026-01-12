using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levelstat_test
{
    internal class AdminUsersModelTests
    {
        [TestMethod]
        public void Test_OnGet_InitializesUsersList()
        {
            // Arrange
            var model = new AdminUsersModel();

            // Act
            model.OnGet();

            // Assert
            Assert.IsNotNull(model.Users);
        }

        [TestMethod]
        public void Test_OnGet_CreatesThreeUsers()
        {
            // Arrange
            var model = new AdminUsersModel();

            // Act
            model.OnGet();

            // Assert
            Assert.AreEqual(3, model.Users.Count);
        }

        [TestMethod]
        public void Test_OnGet_ContainsAdminUser()
        {
            // Arrange
            var model = new AdminUsersModel();

            // Act
            model.OnGet();

            // Assert
            var admin = model.Users.FirstOrDefault(u => u.Role == "Admin");
            Assert.IsNotNull(admin);
            Assert.AreEqual("admin", admin.Username);
        }

        [TestMethod]
        public void Test_OnGet_ContainsInactiveUser()
        {
            // Arrange
            var model = new AdminUsersModel();

            // Act
            model.OnGet();

            // Assert
            bool hasInactive = model.Users.Any(u => u.Active == false);
            Assert.IsTrue(hasInactive);

        }
    }
}
