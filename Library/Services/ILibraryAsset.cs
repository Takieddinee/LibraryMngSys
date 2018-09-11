using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);
        void Add(LibraryAsset libraryAsset);
        String getAuthorOrDirector(int id);
        String getDeweyIndex(int id);
        String getTitle(int id);
        String getIsbn(int id);
        String getType(int id);
        LibraryBranch GetLibraryBranch(int id);


    }
}
