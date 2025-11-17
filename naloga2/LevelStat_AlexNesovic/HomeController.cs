namespace LevelStat_AlexNesovic
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString("user");
            if (user == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.User = user;
            return View();
        }
    }

}
