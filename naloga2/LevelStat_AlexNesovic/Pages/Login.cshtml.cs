using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LevelStat_AlexNesovic.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

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
            return RedirectToPage("/Index");
        }
    }
}
