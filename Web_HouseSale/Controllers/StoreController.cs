using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_HouseSale.Models;      // đổi nếu AppDbContext/Product ở namespace khác
using Web_HouseSale.Data;        // đổi nếu DbContext ở nơi khác

namespace Web_HouseSale.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        // GET: /Store
        [HttpGet]
        public ActionResult Index(int page = 1, int sort = 1, string[] brands = null)
        {
            const int pageSize = 9;

            var q = _db.Products.AsQueryable();

            // --- FILTER BY BRAND (Category) robust: Trim + Lower ---
            if (brands != null && brands.Length > 0)
            {
                var set = new HashSet<string>(brands
                    .Where(b => !string.IsNullOrWhiteSpace(b))
                    .Select(b => b.Trim().ToLower()));

                q = q.Where(p => set.Contains(((p.Category ?? "").Trim().ToLower())));
            }

            // --- SORT ---
            switch (sort)
            {
                case 2: q = q.OrderBy(p => p.Id); break;                      // Oldest
                case 3: q = q.OrderByDescending(p => p.Price); break;         // Price high->low
                case 4: q = q.OrderBy(p => p.Price); break;                   // Price low->high
                default: q = q.OrderByDescending(p => p.Id); break;           // Newest (mặc định)
            }

            // --- PAGINATION ---
            var total = q.Count();
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);
            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var items = q.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // ViewBags cho View
            ViewBag.Page = page;
            ViewBag.Sort = sort;
            ViewBag.Total = total;
            ViewBag.TotalPages = totalPages;
            ViewBag.SelectedBrands = brands ?? new string[0];

            return View(items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
