using LevelStat_AlexNesovic.Helpers;
using Levelstat_test.TestHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Levelstat_test;

[TestClass]
public class ActivityLogTests
{
    private DefaultHttpContext NewHttp()
    {
        var http = new DefaultHttpContext();
        http.Session = new TestSession();
        return http;
    }

    [TestMethod]
    public void Test_Get_WhenEmpty_ReturnsEmptyList()
    {
        var http = NewHttp();

        var list = ActivityLog.Get(http);

        Assert.IsNotNull(list);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void Test_Add_AddsNewestFirst()
    {
        var http = NewHttp();

        ActivityLog.Add(http, "A1");
        ActivityLog.Add(http, "A2");

        var list = ActivityLog.Get(http);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("A2", list[0].Message);
        Assert.AreEqual("A1", list[1].Message);
    }

    [TestMethod]
    public void Test_Clear_RemovesLog()
    {
        var http = NewHttp();

        ActivityLog.Add(http, "X");
        ActivityLog.Clear(http);

        var list = ActivityLog.Get(http);
        Assert.AreEqual(0, list.Count);
    }
}
