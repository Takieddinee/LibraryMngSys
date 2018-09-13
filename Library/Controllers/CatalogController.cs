using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.ViewModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;
        public CatalogController (ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();

            var model = assetModels.Select(
                    result => new AssetIndexModel
                    {
                        Id = result.Id,
                        ImageUrl = result.ImageUrl,
                        Title = result.Titel,
                        NumberOfCopies = result.NumberOfCopies,
                        AuthorOrDirector = _assets.getAuthorOrDirector(result.Id),
                        DeweyCallNumber = _assets.getDeweyIndex(result.Id),
                        Type = _assets.getType(result.Id)

                    }
                );


            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var asset = _assets.GetById(id);
            var currentHold = _checkouts.GetHolds(id).Select(a => new AssetHoldModel
            {
                HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id).ToString("d"),
                PatronName = _checkouts.GetCurrentHoldPatronName(a.Id)
            });
            var model = new AssetDetailModel
            {
                Id = id,
                Title = asset.Titel,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.getAuthorOrDirector(id),
                CurrentLocation = _assets.GetLibraryBranch(id).Name,
                DeweyCallNumber = _assets.getDeweyIndex(id),
                ISBN = _assets.getIsbn(id),
                CheckoutHistories = _checkouts.GetCheckoutHistories(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                PatronName = _checkouts.getCurrentCheckoutPatron(id),
                CurrentHold = currentHold


            };
            return View(model);

        }

        public IActionResult Checkout(int id)
        {
            var asset = _assets.GetById(id);
            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Titel,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };

            return View(model);

        }

        public IActionResult Hold(int id)
        {
            var asset = _assets.GetById(id);
            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Titel,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetHolds(id).Count()
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int cardId)
        {
            _checkouts.CheckInItem(assetId, cardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int cardId)
        {
            _checkouts.PlaceHold(assetId, cardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });

        }

        public IActionResult MarkFound(int assetId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });

        }
    }
}
