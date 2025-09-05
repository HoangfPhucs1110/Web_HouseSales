using System.Linq;
using System.Web.Mvc;
using Web_HouseSale.Data;
using Web_HouseSale.Models;

namespace Web_HouseSale.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        public ActionResult Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return HttpNotFound();

            var product = _db.Products.FirstOrDefault(p => p.Slug == slug);
            if (product == null)
                return HttpNotFound();

            return View("Index", product);
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Store");
        }
    }
}
