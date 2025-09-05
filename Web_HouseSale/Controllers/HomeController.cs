using System.Linq;
using System.Web.Mvc;
using Web_HouseSale.Data;
using Web_HouseSale.Models;

public class HomeController : Controller
{
    private readonly AppDbContext _db = new AppDbContext();

    public ActionResult Index()
    {
        var list = _db.Products
            .OrderByDescending(p => p.Id)
            .ToList();
        return View(list);
    }
}
