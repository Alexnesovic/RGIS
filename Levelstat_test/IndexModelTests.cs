using LevelStat_AlexNesovic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levelstat_test
{
    internal class IndexModelTests
    {
        [TestMethod]
        public void Test_UserEmail_DefaultValue_IsNull()
        {
            // Arrange
            var model = new IndexModel();

            // Act
            var email = model.UserEmail;

            // Assert
            Assert.IsNull(email);
        }

        [TestMethod]
        public void Test_UserEmail_CanBeSet()
        {
            // Arrange
            var model = new IndexModel();

            // Act
            model.UserEmail = "test@test.com";

            // Assert
            Assert.AreEqual("test@test.com", model.UserEmail);
        }

        [TestMethod]
        public void Test_Model_CanBeCreated()
        {
            // Act
            var model = new IndexModel();

            // Assert
            Assert.IsNotNull(model);
        }
    }
}
