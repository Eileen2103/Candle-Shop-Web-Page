using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.data;

namespace backend.Controllers;


public class UserController : Controller
{
    private readonly AppDBContext _context;

    public UserController(AppDBContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User u)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(u);
    }
}