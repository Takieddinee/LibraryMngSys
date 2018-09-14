using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        private ICheckout _checkouts;
        private ILibraryAsset _assets;

        public PatronController(ILibraryAsset libraryAsset, ICheckout checkout)
        {
            _checkouts = checkout;
            _assets = libraryAsset;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
