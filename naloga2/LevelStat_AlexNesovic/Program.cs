using Microsoft.AspNetCore.Mvc;

namespace LevelStat_AlexNesovic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }

    // =======================
    // DATA CLASSES
    // =======================

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public static class FakeDb
    {
        public static List<User> Users { get; set; } = new();
    }

    // =======================
    // API CONTROLLER
    // =======================
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (FakeDb.Users.Any(u => u.Email == user.Email))
                return BadRequest("User already exists.");

            FakeDb.Users.Add(user);
            return Ok("Registered.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = FakeDb.Users
                .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok("Login successful.");
        }
    }

    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password);
}
