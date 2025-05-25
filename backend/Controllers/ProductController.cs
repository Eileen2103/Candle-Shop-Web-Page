using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.data;
using backend.Helpers;

namespace backend.Controllers;


public class ProductController : Controller
{
    private readonly AppDBContext _context;

    public ProductController(AppDBContext context)
    {
        _context = context;
    }
    public IActionResult Index(string[] categories, string category)
    {
        // var products = _context.Products.ToList();
        // Console.WriteLine("Toplam ürün sayısı: " + products.Count);
        // return View(products);

        var products = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            category = category.ToLower();
            products = products.Where(p => p.Category != null && p.Category.ToLower() == category);
        }
        else if (categories != null && categories.Any())
        {
            if (!categories.Contains("all"))
            {
                var lowered = categories.Select(c => c.ToLower()).ToList();
                products = products.Where(p => p.Category != null && lowered.Contains(p.Category.ToLower()));

            }

        }

        return View(products.ToList());
    }



    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ID == id);
        if (product == null)
            return NotFound();

        return View(product);
    }


    [HttpPost]
    public IActionResult Create(Product p)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(p);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.ID == productId);
        if (product == null)
            return NotFound();

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        var existingItem = cart.FirstOrDefault(c => c.ProductID == product.ID);
        if (existingItem != null)
            existingItem.Quantity++;
        else
            cart.Add(new CartItem
            {
                ProductID = product.ID,
                Name = product.Name,
                Price = product.OrderPrice,
                Quantity = 1
            });

        HttpContext.Session.SetObjectAsJson("Cart", cart);

        return RedirectToAction("Index", "Product");
    }

}