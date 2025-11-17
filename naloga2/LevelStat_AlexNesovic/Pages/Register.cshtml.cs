using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LevelStat_AlexNesovic.Program;

namespace LevelStat_AlexNesovic.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string Error { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (FakeDb.Users.Any(x => x.Email == Email))
            {
                Error = "Uporabnik že obstaja!";
                return Page();
            }

            FakeDb.Users.Add(new User { Email = Email, Password = Password });
            return RedirectToPage("/Login");
        }
    }
}
