using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        ViewBag.CartTotal = cart.Sum(i => i.Price * i.Quantity);
        return View(cart);
    }

    public IActionResult Remove(int productId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var item = cart.FirstOrDefault(i => i.ProductID == productId);
        if (item != null)
        {
            cart.Remove(item);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Increase(int productId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var item = cart.FirstOrDefault(i => i.ProductID == productId);
        if (item != null)
        {
            item.Quantity++;
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Decrease(int productId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var item = cart.FirstOrDefault(i => i.ProductID == productId);
        if (item != null && item.Quantity > 1)
        {
            item.Quantity--;
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Checkout()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        if (cart.Count == 0)
            return RedirectToAction("Index");

        HttpContext.Session.Remove("Cart");

        TempData["OrderMessage"] = "We got your order!";
        return RedirectToAction("Index", "Product");
    }

}
