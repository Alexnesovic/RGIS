using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using LevelStat_AlexNesovic.Helpers;


namespace LevelStat_AlexNesovic.Pages
{
    public class ProfileModel : PageModel
    {
        public string Email { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Ime je obvezno.")]
        [StringLength(40, ErrorMessage = "Ime je lahko dolgo največ 40 znakov.")]
        public string DisplayName { get; set; } = "";

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // ✅ zaščita: mora biti prijavljen
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Login");

            Email = email;

            // preberi ime iz session (ali nastavi privzeto)
            DisplayName = HttpContext.Session.GetString("DisplayName") ?? "Uporabnik";
            return Page();
        }

        public IActionResult OnPost()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToPage("/Login");

            Email = email;

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Popravi napake v obrazcu.";
                return Page();
            }

            // ✅ shrani ime v Session
            HttpContext.Session.SetString("DisplayName", DisplayName.Trim());
            ActivityLog.Add(HttpContext, "Posodobitev profila");
            SuccessMessage = "Profil uspešno posodobljen.";
            return Page();
        }
    }
}
