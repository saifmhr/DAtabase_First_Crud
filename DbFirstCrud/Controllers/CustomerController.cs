using DbFirstCrud.Data;
using DbFirstCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DbFirstCrud.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DbFirstCrudContext _Context;

        public CustomerController(DbFirstCrudContext context)
        {
            _Context = context;
        }
        public IActionResult Index(int pg = 1)
        {
            const int pageSize = 5;

            if (pg < 1)
                pg = 1;

            int recsCount = _Context.Customers.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recsSkip = (pg - 1) * pageSize;

            List<Customer> customers = _Context.Customers.Skip(recsSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(customers);
        }

        public IActionResult Detail(string id)
        {

            Customer customer = _Context.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Customer customer = _Context.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _Context.Attach(customer);
            _Context.Entry(customer).State = EntityState.Modified; 
            _Context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            Customer customer = _Context.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _Context.Attach(customer);
            _Context.Entry(customer).State = EntityState.Deleted;
            _Context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            //var customerId = _Context.Customers.Max(custid => custid.CustomerId);
            //long customerNo;

            //Int64.TryParse(customerId.Substring(2,customerId.Length-2), out customerNo);
            //if(customerNo > 0)
            //{
            //    customerNo= customerNo + 1;
            //    customerId="Cs"+ customerNo.ToString();

            //}
           // customer.CustomerId = customerId;
            _Context.Attach(customer);
            _Context.Entry(customer).State = EntityState.Added;
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
