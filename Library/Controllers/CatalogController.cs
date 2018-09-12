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
    }
}
