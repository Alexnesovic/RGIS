using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

public class UserStatsModel : PageModel
{
    public string Username { get; set; }
    public int LoginCount { get; set; }
    public DateTime LastLogin { get; set; }
    public int CompletedTasks { get; set; }

    public void OnGet()
    {
        // Dummy podatki – lahko povežem na DB če želiš
        Username = "testUser";
        LoginCount = 14;
        LastLogin = DateTime.Now.AddHours(-5);
        CompletedTasks = 7;
    }
}
