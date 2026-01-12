using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LevelStat_AlexNesovic.Helpers;


namespace LevelStat_AlexNesovic.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        // ✅ DODAJ
        [BindProperty] public bool RememberMe { get; set; }

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // Če je uporabnik že prijavljen, ga pošlji na Index
            var sessionEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(sessionEmail))
                return RedirectToPage("/Index");

            ActivityLog.Add(HttpContext, $"Prijava: {Email}");


            // ✅ Auto-login iz cookie (če obstaja)
            if (Request.Cookies.TryGetValue("RememberMeEmail", out var rememberedEmail)
                && !string.IsNullOrWhiteSpace(rememberedEmail))
            {
                HttpContext.Session.SetString("UserEmail", rememberedEmail);
                return RedirectToPage("/Index");
                ActivityLog.Add(HttpContext, $"Samodejna prijava (Remember me): {rememberedEmail}");

            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var user = FakeDb.Users
                .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user == null)
            {
                ErrorMessage = "Napačni podatki!";
                return Page();
            }

            HttpContext.Session.SetString("UserEmail", Email);

            // ✅ Če je checkbox označen, shrani cookie za 30 dni
            if (RememberMe)
            {
                Response.Cookies.Append(
                    "RememberMeEmail",
                    Email,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        HttpOnly = true,
                        Secure = true,               // deluje na https
                        SameSite = SameSiteMode.Lax
                    }
                );
            }

            return RedirectToPage("/Index");
        }
    }
}
