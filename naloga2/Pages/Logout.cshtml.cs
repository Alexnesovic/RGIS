using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LevelStat_AlexNesovic.Helpers;

namespace LevelStat_AlexNesovic.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(email))
                ActivityLog.Add(HttpContext, $"Odjava: {email}");

            HttpContext.Session.Clear();

            // ✅ DODAJ
            Response.Cookies.Delete("RememberMeEmail");

            return RedirectToPage("/Index");
        }
    }
}
