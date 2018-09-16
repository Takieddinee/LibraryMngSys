using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.ViewModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        private Ibranch _ibranch;

        public BranchController(Ibranch ibranch)
        {
            _ibranch = ibranch;
        }
        public IActionResult Index()
        {
            var model = _ibranch.GetAll()
                .Select(br => new BranchModel
                {
                    Id = br.Id,
                    BranchName = br.Name,
                    NumberOfAssets = _ibranch.GetAssets(br.Id).Count(),
                    NumberOfPatrons = _ibranch.GetPatrons(br.Id).Count(),
                    IsOpen = _ibranch.IsOpen(br.Id)
                }).ToList();

           

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _ibranch.Get(id);
            var model = new BranchModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Adress,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfPatrons = _ibranch.GetPatrons(id).Count(),
                NumberOfAssets = _ibranch.GetAssets(id).Count(),
                TotalAssetValue = _ibranch.GetAssetsValue(id),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _ibranch.GetLibraryHours(id)
            };

            return View(model);
        }
    }
}