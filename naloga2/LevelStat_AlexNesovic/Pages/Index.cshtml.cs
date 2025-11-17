using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LevelStat_AlexNesovic.Pages
{
    public class IndexModel : PageModel
    {
        public string? UserEmail { get; set; }

        public void OnGet()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");
        }
    }
}
