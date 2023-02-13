using DbFirstCrud.Data;
using DbFirstCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbFirstCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbFirstCrudContext _context;
        public ProductController(DbFirstCrudContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pg = 1)
        {
            const int pageSize = 5;

            if (pg < 1)
                pg = 1;

            int recsCount = _context.Products.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recsSkip = (pg - 1) * pageSize;

            List<Product> products = _context.Products.Skip(recsSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
