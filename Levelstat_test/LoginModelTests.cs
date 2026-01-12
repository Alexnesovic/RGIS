using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LevelStat_AlexNesovic.Pages;
using Moq;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

[TestClass]
public class LoginModelTests
{
    private LoginModel CreateModel()
    {
        var context = new DefaultHttpContext();

        var sessionMock = new Mock<ISession>();
        var sessionStorage = new Dictionary<string, byte[]>();

        sessionMock
            .Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
            .Callback<string, byte[]>((key, value) =>
            {
                sessionStorage[key] = value;
            });

        sessionMock
            .Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
            .Returns((string key, out byte[] value) =>
            {
                return sessionStorage.TryGetValue(key, out value);
            });

        context.Session = sessionMock.Object;

        return new LoginModel
        {
            PageContext = new PageContext
            {
                HttpContext = context
            }
        };
    }


    [TestMethod]
    public void Test_OnPost_ReturnsPage_WhenUserIsInvalid()
    {
        // Arrange
        var model = CreateModel();
        model.Email = "wrong@test.com";
        model.Password = "wrong";

        // Act
        var result = model.OnPost();

        // Assert
        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual("Napačni podatki!", model.ErrorMessage);
    }

    

    [TestMethod]
    public void Test_OnPost_DoesNotSetSession_WhenLoginFails()
    {
        // Arrange
        var model = CreateModel();
        model.Email = "bad@test.com";
        model.Password = "bad";

        // Act
        model.OnPost();

        // Assert
        Assert.IsNull(model.HttpContext.Session.GetString("UserEmail"));
    }
}
