using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class AdminUsersModel : PageModel
{
    public List<UserInfo> Users { get; set; }

    public void OnGet()
    {
        // Dummy podatki – lahko povežem na pravo DB
        Users = new List<UserInfo>
        {
            new UserInfo{ Username="testUser", Email="user@test.com", Role="User", Active=true},
            new UserInfo{ Username="admin", Email="admin@test.com", Role="Admin", Active=true},
            new UserInfo{ Username="student1", Email="student1@test.com", Role="User", Active=false}
        };
    }
}

public class UserInfo
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool Active { get; set; }
}
