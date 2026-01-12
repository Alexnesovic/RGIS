using LevelStat_AlexNesovic.Pages;
using LevelStat_AlexNesovic.Helpers;
using Levelstat_test.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Levelstat_test;

[TestClass]
public class ProfileModelTests
{
    [TestMethod]
    public void Test_OnGet_WhenNotLoggedIn_RedirectsToLogin()
    {
        var model = PageModelFactory.Create<ProfileModel>();

        var result = model.OnGet();

        Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        Assert.AreEqual("/Login", ((RedirectToPageResult)result).PageName);
    }

    [TestMethod]
    public void Test_OnGet_WhenLoggedIn_SetsEmailAndDefaultName()
    {
        var model = PageModelFactory.Create<ProfileModel>(http =>
        {
            http.Session.SetString("UserEmail", "a@b.com");
            // DisplayName ni nastavljen → pričakujemo "Uporabnik"
        });

        var result = model.OnGet();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual("a@b.com", model.Email);
        Assert.AreEqual("Uporabnik", model.DisplayName);
    }

    [TestMethod]
    public void Test_OnPost_WhenValid_SavesName_AndAddsActivity()
    {
        var model = PageModelFactory.Create<ProfileModel>(http =>
        {
            http.Session.SetString("UserEmail", "a@b.com");
        });

        model.DisplayName = "Alex";

        var result = model.OnPost();

        Assert.IsInstanceOfType(result, typeof(PageResult));
        Assert.AreEqual("Alex", model.HttpContext.Session.GetString("DisplayName"));

        // preveri, da se je dodala aktivnost
        var activities = ActivityLog.Get(model.HttpContext);
        Assert.IsTrue(activities.Count >= 1);
        Assert.AreEqual("Posodobitev profila", activities[0].Message);
    }
}
