using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customers = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customers = customerModel.Customers,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            try
            {
                if (customerModel.Customers.Id == 0)
                    _context.customers.Add(customerModel.Customers);
                else
                {
                    var customerInDb = _context.customers.Single(c => c.Id == customerModel.Customers.Id);
                    customerInDb.Name = customerModel.Customers.Name;
                    customerInDb.BirthDate = customerModel.Customers.BirthDate;
                    customerInDb.MembershipTypeId = customerModel.Customers.MembershipTypeId;
                    customerInDb.IsSubscribedToNewsletter = customerModel.Customers.IsSubscribedToNewsletter;
                }

                _context.SaveChanges();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            

            return RedirectToAction("Index", "Customers");
        }

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customers = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}