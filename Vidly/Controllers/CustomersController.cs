using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c=>c.MemberShipType).ToList();
            return View();
        }

        public ActionResult New()
        {
            var membershiptypes = _context.MemberShipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer=new Customer(),
                MemberShipTypes = membershiptypes
            };
            return View("CustomerForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(m =>m.id==id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer=customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
            };
                return View("CustomerForm", viewModel);
                
            }
            if(customer.id==0)
                _context.Customers.Add(customer);
            else
            {
                var CustomerInDB = _context.Customers.Single(c => c.id == customer.id);
                CustomerInDB.Name = customer.Name;
                CustomerInDB.Birthdate = customer.Birthdate;
                CustomerInDB.MemberShipTypeId = customer.MemberShipTypeId;
                CustomerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
               
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Details(int id)
        {
            
           var customer=_context.Customers.Include(c => c.MemberShipType).FirstOrDefault(customerx => customerx.id == id);
           if(customer==null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
    }
}