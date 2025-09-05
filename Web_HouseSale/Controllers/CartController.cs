using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_HouseSale.Data;
using Web_HouseSale.Models;

namespace Web_HouseSale.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, string color, string storage, int quantity = 1)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product == null) return HttpNotFound();

                var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

                var existing = cart.FirstOrDefault(c =>
                    c.Product.Id == id &&
                    string.Equals(c.Color, color) &&
                    string.Equals(c.Storage, storage));

                if (existing != null)
                    existing.Quantity += quantity;
                else
                    cart.Add(new CartItem { Product = product, Color = color, Storage = storage, Quantity = quantity });

                Session["Cart"] = cart;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult UpdateQuantity(int id, string color, string storage, int quantity)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.Product.Id == id && c.Color == color && c.Storage == storage);
                if (item != null)
                {
                    item.Quantity = quantity;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveItem(int id, string color, string storage)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                cart.RemoveAll(c => c.Product.Id == id && c.Color == color && c.Storage == storage);
                Session["Cart"] = cart;
            }
            return RedirectToAction("Index");
        }
    }
}
