using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class UserStatsModelTests
{
    [TestMethod]
    public void Test_OnGet_SetsUsername()
    {
        // Arrange
        var model = new UserStatsModel();

        // Act
        model.OnGet();

        // Assert
        Assert.AreEqual("testUser", model.Username);
    }

    [TestMethod]
    public void Test_OnGet_SetsLoginCount()
    {
        // Arrange
        var model = new UserStatsModel();

        // Act
        model.OnGet();

        // Assert
        Assert.AreEqual(14, model.LoginCount);
    }

    [TestMethod]
    public void Test_OnGet_SetsCompletedTasks()
    {
        // Arrange
        var model = new UserStatsModel();

        // Act
        model.OnGet();

        // Assert
        Assert.AreEqual(7, model.CompletedTasks);
    }

}
