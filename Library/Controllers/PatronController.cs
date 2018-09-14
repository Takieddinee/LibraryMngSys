using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Library.Models.ViewModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class PatronController : Controller
    {

        private IPatron _patron;

        public PatronController(IPatron patron)
        {

            _patron = patron;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            var patrons = _patron.GetAll();
            var model = patrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName,
                LibraryCardId = p.LibraryCard.Id,

                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name
            }).ToList();
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var p = _patron.Get(id);

            var model = new PatronDetailModel
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName,
                LibraryCardId = p.LibraryCard.Id,
                OverdueFees = p.LibraryCard.Fees,
                HomeLibraryBranch = p.HomeLibraryBranch.Name,
                Adress = p.Adress,
                Telephone =p.TelephoneNumber,
                AssetsCheckouts = _patron.GetCheckouts(id).ToList()?? new List<Checkout>(),
                checkoutHistories = _patron.GetCheckoutHistories(id).ToList()?? new List<CheckoutHistory>(),
                Holds = _patron.GetHolds(id).ToList() ?? new List<Hold>()
            };

            return View(model);
        }
    }
}
