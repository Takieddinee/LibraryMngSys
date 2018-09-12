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
        public CatalogController (ILibraryAsset assets)
        {
            _assets = assets;
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
                ISBN = _assets.getIsbn(id)


            };
            return View(model);

        }
    }
}
