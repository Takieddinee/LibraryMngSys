using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BranchServicecs : Ibranch
    {
        private LibraryContext _context;
        public BranchServicecs(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }
        public void Add(LibraryBranch libraryBranch)
        {
            _context.Add(libraryBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            return _context.LibraryBranches
                .Include(lb => lb.Patrons)
                .Include(lb => lb.LibraryAssets)
                .FirstOrDefault(lb => lb.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                            .Include(lb => lb.Patrons)
                            .Include(lb => lb.LibraryAssets);
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return Get(branchId).LibraryAssets;
        }

        public IEnumerable<string> GetLibraryHours(int branchId)
        {
            var hours = _context.BranchHours.Where(a => a.Branch.Id == branchId);

            var displayHours =
                DataHelpers.HumanizeBusinessHours(hours);

            return displayHours;
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return Get(branchId).Patrons;
        }

        public bool IsOpen(int branchId)
        {
            var nowH = DateTime.Now.Hour;
            var nowD = (int) DateTime.Now.DayOfWeek+1;
            var hours = _context.BranchHours.Where(h => h.Branch.Id == branchId);
            var dayHours = hours.FirstOrDefault(h => h.DayOfWeek == nowD);

            var isOpen = nowH < dayHours.CloseTime && nowH > dayHours.OpenTime;

            return isOpen;



        }

        public decimal GetAssetsValue(int branchId)
        {
            var assetsValue = GetAssets(branchId).Select(a => a.Cost);
            return assetsValue.Sum();
        }
    }
}
