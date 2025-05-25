using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.data;
using System.Linq;

namespace backend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDBContext _context;

    public HomeController(ILogger<HomeController> logger, AppDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Main Page";

        var topRatedProducts = _context.Products
                   .OrderByDescending(p => (double)(p.Rating ?? 0))
                   .Take(3)
                   .ToList();

        return View(topRatedProducts);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        ViewData["Title"] = "About Us";
        return View();
    }
    public IActionResult Soy()
    {
        ViewData["Title"] = "What is Soy?";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
