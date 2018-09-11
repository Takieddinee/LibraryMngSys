using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext libraryContext)
        {
            _context = libraryContext;
        }

        public void Add(LibraryAsset libraryAsset)
        {
            _context.Add(libraryAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);

        }

        public string getAuthorOrDirector(int id)
        {
            if (_context.Book.Any(book => book.Id == id))
            {
                return _context.Book.FirstOrDefault(book => book.Id == id).Author;
            }
            else if (_context.Videos.Any(video => video.Id == id))
            {
                return _context.Videos.FirstOrDefault(video => video.Id == id).Director;
            }
            else return "";
        }

        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id);
        }

        public string getDeweyIndex(int id)
        {
            if (_context.Book.Any(book => book.Id == id))
            {
                return _context.Book.FirstOrDefault(book => book.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string getIsbn(int id)
        {
            if (_context.Book.Any(book => book.Id == id))
            {
                return _context.Book.FirstOrDefault(book => book.Id == id).ISBN;
            }
            else return "";
        }

        public LibraryBranch GetLibraryBranch(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;

        }

        public string getTitle(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Titel;
        }

        public string getType(int id)
        {
            throw new NotImplementedException();
        }
    }
}
