using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.data;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace backend.Controllers;

public class AccountController : Controller
{
    private readonly AppDBContext _context;

    public AccountController(AppDBContext context)
    {
        _context = context;
    }


    public IActionResult Profile()
    {
        var email = User.Identity?.Name;
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return RedirectToAction("Login");
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Login() => View();


    [HttpPost]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.password == Password);

        if (user != null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.Name),
            new Claim("UserId", user.ID.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            TempData["LoginMessage"] = "Welcome";
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Incorrect email or password";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }



    [HttpGet("register")]
    public IActionResult Register() => View();


    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.Name),
            new Claim("UserId", user.ID.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            TempData["RegisterMessage"] = "Signed in Successfully.";
            return RedirectToAction("Index", "Home");
        }

        return View(user);
    }


}
