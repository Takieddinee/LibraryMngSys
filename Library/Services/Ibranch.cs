using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface Ibranch
    {
        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<Patron> GetPatrons(int branchId);
        IEnumerable<LibraryAsset> GetAssets(int branchId);
        LibraryBranch Get(int branchId);
        void Add(LibraryBranch libraryBranch);
        IEnumerable<String> GetLibraryHours(int branchId);
        bool IsOpen(int branchId);
        decimal GetAssetsValue(int branchId);
    }
}
