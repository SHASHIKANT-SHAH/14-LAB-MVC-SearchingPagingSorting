using DAL;
using LAB_MVC_SearchingPagingSorting.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LAB_MVC_SearchingPagingSorting.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        { 
            _context = context;
        }
        public IActionResult Index(string SearchText = "", int page=1, int pageSize = 4, string sortCol = "ProductId", string sortDir = "asc")
        {
            ViewData["sortCol"] = sortCol; 
            ViewData["sortDir"] = sortDir;

            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(SearchText))
            {
                products = _context.Products.Where(p => p.Name.Contains(SearchText)).AsQueryable();
            }

            if (page < 1)
                page = 1;

            switch (sortCol)
            {
                case "Name":
                    products = sortDir == "asc" ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                    break;
                case "Description":
                    products = sortDir == "asc" ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    break;
                case "Color": products = sortDir == "asc" ? products.OrderBy(p => p.Color) : products.OrderByDescending(p => p.Color); 
                    break;
                case "UnitPrice":
                    products = sortDir == "asc" ? products.OrderBy(p => p.UnitPrice) : products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    products = sortDir == "asc" ? products.OrderBy(p => p.ProductId) : products.OrderByDescending(p => p.ProductId); 
                    break;
            }
            int totalRecords = products.Count();
            var data = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            Pager pager = new Pager(totalRecords, page, pageSize);
            ViewBag.Pager = pager;

            return View(data);
        }
    }
}
