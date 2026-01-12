using LevelStat_AlexNesovic.Pages;
using Levelstat_test.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Levelstat_test;

[TestClass]
public class LogoutModelTests
{
    [TestMethod]
    public void Test_OnGet_ClearsSession()
    {
        var model = PageModelFactory.Create<LogoutModel>(http =>
        {
            http.Session.SetString("UserEmail", "a@b.com");
            http.Session.SetString("DisplayName", "Alex");
        });

        model.OnGet();

        Assert.IsNull(model.HttpContext.Session.GetString("UserEmail"));
        Assert.IsNull(model.HttpContext.Session.GetString("DisplayName"));
    }

    [TestMethod]
    public void Test_OnGet_DeletesRememberMeCookie()
    {
        var model = PageModelFactory.Create<LogoutModel>();

        model.OnGet();

        // Cookie delete gre v response header Set-Cookie
        var setCookie = model.HttpContext.Response.Headers["Set-Cookie"].ToString();
        Assert.IsTrue(setCookie.Contains("RememberMeEmail="), "Set-Cookie ne vsebuje RememberMeEmail");
    }

    [TestMethod]
    public void Test_OnGet_RedirectsToIndex()
    {
        var model = PageModelFactory.Create<LogoutModel>();

        var result = model.OnGet();

        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);
    }
}
