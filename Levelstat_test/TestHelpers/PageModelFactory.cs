using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Levelstat_test.TestHelpers;

public static class PageModelFactory
{
    public static T Create<T>(Action<DefaultHttpContext>? configure = null)
        where T : PageModel, new()
    {
        var http = new DefaultHttpContext();
        http.Session = new TestSession();
        configure?.Invoke(http);

        var actionContext = new ActionContext(http, new RouteData(), new ActionDescriptor());
        var pageContext = new PageContext(actionContext);

        var model = new T { PageContext = pageContext };
        return model;
    }
}
