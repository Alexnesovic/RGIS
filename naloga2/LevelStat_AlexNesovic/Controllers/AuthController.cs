namespace LevelStat_AlexNesovic.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AuthController : Controller
    {
        // PONAREJENA “BAZA”
        private static List<(string Email, string Password)> Users = new();

        // GET: /auth/register
        public IActionResult Register() => View();

        // POST: /auth/register
        [HttpPost]
        public IActionResult Register(string email, string password)
        {
            Users.Add((email, password));
            return RedirectToAction("Login");
        }

        // GET: /auth/login
        public IActionResult Login() => View();

        // POST: /auth/login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user.Email == null)
            {
                ViewBag.Error = "Napačni podatki!";
                return View();
            }

            // ZAPOMNI SI GA V SEJI
            HttpContext.Session.SetString("user", email);
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
