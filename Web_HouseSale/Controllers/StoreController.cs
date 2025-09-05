using System;
using System.Linq;
using System.Web.Mvc;
using Web_HouseSale.Data;
using Web_HouseSale.Models;

public class StoreController : Controller
{
    private readonly AppDbContext _db = new AppDbContext();

    // /Store/Index?page=1&sort=1
    public ActionResult Index(int page = 1, int sort = 1)
    {
        const int pageSize = 9;

        var q = _db.Products.AsQueryable();

        // sort: 1=Newest, 2=Oldest, 3=Price desc, 4=Price asc
        switch (sort)
        {
            case 2: q = q.OrderBy(p => p.Id); break;
            case 3: q = q.OrderByDescending(p => p.Price); break;
            case 4: q = q.OrderBy(p => p.Price); break;
            default: q = q.OrderByDescending(p => p.Id); break;
        }

        var total = q.Count();
        var items = q.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.Total = total;
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
        ViewBag.Sort = sort;

        return View(items);
    }
}
