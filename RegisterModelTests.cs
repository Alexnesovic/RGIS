using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using LevelStat_AlexNesovic.Pages;
using static LevelStat_AlexNesovic.Program;
using LevelStat_AlexNesovic;
using Microsoft.AspNetCore.Mvc.RazorPages;

[TestClass]
public class RegisterModelTests
{
    [TestInitialize]
    public void Setup()
    {
        FakeDb.Users.Clear();
        FakeDb.Users.Add(new User
        {
            Email = "existing@test.com",
            Password = "1234"
        });
    }

    [TestMethod]
    public void Test_OnPost_ReturnsPage_WhenUserAlreadyExists()
    {
        // Arrange
        var model = new RegisterModel
        {
            Email = "existing@test.com",
            Password = "pass"
        };

        // Act
        var result = model.OnPost();

        // Assert
        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual("Uporabnik že obstaja!", model.Error);
    }

    [TestMethod]
    public void Test_OnPost_Redirects_WhenUserDoesNotExist()
    {
        // Arrange
        var model = new RegisterModel
        {
            Email = "new@test.com",
            Password = "1234"
        };

        // Act
        var result = model.OnPost();

        // Assert
        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
    }

   
}
