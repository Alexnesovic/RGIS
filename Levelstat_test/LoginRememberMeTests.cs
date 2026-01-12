using LevelStat_AlexNesovic.Pages;
using Levelstat_test.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Levelstat_test;

[TestClass]
public class LoginRememberMeTests
{
    [TestMethod]
    public void Test_OnGet_WhenRememberMeCookieExists_AutoLogsIn()
    {
        var model = PageModelFactory.Create<LoginModel>(http =>
        {
            // cookie za request
            http.Request.Headers["Cookie"] = "RememberMeEmail=a@b.com";
        });

        var result = model.OnGet();

        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual("a@b.com", model.HttpContext.Session.GetString("UserEmail"));
    }

   

    [TestMethod]
    public void Test_OnPost_WhenRememberMeFalse_DoesNotSetCookie()
    {
        var model = PageModelFactory.Create<LoginModel>();

        model.Email = "a@b.com";
        model.Password = "123";
        model.RememberMe = false;

        model.OnPost();

        var setCookie = model.HttpContext.Response.Headers["Set-Cookie"].ToString();
        Assert.IsFalse(setCookie.Contains("RememberMeEmail="));
    }
}
