using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.data;

namespace backend.Controllers;


public class OrderController : Controller
{
    private readonly AppDBContext _context;

    public OrderController(AppDBContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var orders = _context.Orders.ToList();
        return View(orders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Order o)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(o);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(o);
    }
}